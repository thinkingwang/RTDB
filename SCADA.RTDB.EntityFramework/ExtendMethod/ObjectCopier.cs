using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SCADA.RTDB.EntityFramework.ExtendMethod
{
    /// <summary>
    /// 复制对象类
    /// </summary>
    public class ObjectCopier
    {
        /// <summary>
        /// 复制对象属性
        /// </summary>
        /// <param name="dstObject">目标对象</param>
        /// <param name="srcObject">源对象</param>
        public static void CopyProperties(Object dstObject, object srcObject)
        {
            if (dstObject == null)
            {
                return;
            }
            if (srcObject == null)
            {
                return;
            }
            if (srcObject.GetType() != dstObject.GetType())
            {
                throw new Exception("目标对象与源对象类型不一致，不能复制");
            }
            try
            {
                //   clone   properties 
                PropertyDescriptorCollection srcPdc = TypeDescriptor.GetProperties(srcObject);
                PropertyDescriptorCollection dstPdc = TypeDescriptor.GetProperties(dstObject);

                for (int i = 0; i < srcPdc.Count; i++)
                {
                    dstPdc[srcPdc[i].Name].SetValue(dstObject, srcPdc[i].GetValue(srcObject));
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 复制对象属性，只能复制公共属性
        /// </summary>
        /// <param name="dstObject">目标对象</param> 
        /// <param name="propertyName">需要修改的属性</param>
        /// <param name="propertyValue">源对象</param>
        public static void CopyProperty(Object dstObject, string propertyName, object propertyValue)
        {
            if (dstObject == null)
            {
                return;
            }
            try
            {
                PropertyDescriptorCollection dstPdc = TypeDescriptor.GetProperties(dstObject);

                PropertyDescriptor dstPd = dstPdc.Find(propertyName, true);
                if (dstPd == null)
                {
                    throw new ArgumentNullException("需要修改的对象属性不存在");
                }
                if (propertyValue == null)
                {
                    if (!dstPd.PropertyType.IsValueType)
                    {
                        dstPd.SetValue(dstObject, propertyValue);
                        return;
                    }
                    throw new Exception("需要修改的属性类型不能置null");
                }
                if (dstPd.PropertyType != propertyValue.GetType() &&
                    !propertyValue.GetType().IsSubclassOf(dstPd.PropertyType))
                {
                    throw new Exception("需要修改的属性类型不一致");
                }
                dstPd.SetValue(dstObject, propertyValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 复制对象属性，允许拷贝私有属性
        /// </summary>
        /// <param name="dstObject">目标对象</param> 
        /// <param name="propertyName">需要修改的属性</param>
        /// <param name="propertyValue">源对象</param>
        /// <param name="isCopyNonPublic">是否拷贝私有属性,需要拷贝私有属性置true</param>
        public static void CopyProperty(Object dstObject, string propertyName, object propertyValue, bool isCopyNonPublic)
        {
            if (dstObject == null)
            {
                return;
            }
            try
            {
                Type t = dstObject.GetType();
                BindingFlags flags = BindingFlags.Instance | BindingFlags.Public;
                if (isCopyNonPublic)
                {
                    flags |= BindingFlags.NonPublic;
                }
                PropertyInfo pI = t.GetProperty(propertyName, flags);
                if (pI == null)
                {
                    throw new ArgumentNullException("需要修改的对象属性不存在");
                }
                if (propertyValue == null)
                {
                    if (!dstObject.GetType().IsValueType)
                    {
                        pI.SetValue(dstObject, null, null);
                        return;
                    }
                    throw new Exception("需要修改的属性类型不能置null");
                }
                if (pI.PropertyType != propertyValue.GetType() &&
                    !propertyValue.GetType().IsSubclassOf(pI.PropertyType))
                {
                    throw new Exception("需要修改的属性类型不一致");
                }
                pI.SetValue(dstObject, propertyValue, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Perform a deep Copy of the object对象深拷贝.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException(@"The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// 克隆对象
        /// </summary>
        /// <param name="srcCtl">需要克隆的对象</param>
        /// <returns>返回克隆后的新对象</returns>
        public static object CloneObject(object srcCtl)
        {
            try
            {
                if (srcCtl == null)
                {
                    return null;
                }
                Type t = srcCtl.GetType();
                Object dstCtl = Activator.CreateInstance(t);

                //   clone   properties 
                PropertyDescriptorCollection srcPdc = TypeDescriptor.GetProperties(srcCtl);
                PropertyDescriptorCollection dstPdc = TypeDescriptor.GetProperties(dstCtl);

                for (int i = 0; i < srcPdc.Count; i++)
                {

                    if (srcPdc[i].Attributes.Contains(DesignerSerializationVisibilityAttribute.Content))
                    {

                        var collectionVal = srcPdc[i].GetValue(srcCtl);
                        var val = collectionVal as IList<object>;
                        if (val != null)
                        {
                            foreach (var child in val)
                            {
                                object newChild = CloneObject(child);
                                object dstCollectionVal = dstPdc[i].GetValue(dstCtl);
                                var objects = (IList<object>)dstCollectionVal;
                                if (objects != null)
                                    objects.Add(newChild);
                            }
                        }
                    }
                    else
                    {
                        dstPdc[srcPdc[i].Name].SetValue(dstCtl, srcPdc[i].GetValue(srcCtl));
                    }
                }

                return dstCtl;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }

        }
    }
}