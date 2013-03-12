using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using CopyProPerties;

namespace CopyProPerties
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] a = new[] { 1, 2, 3 };
            int[] a1 = new[] { 121, 22, 33 };
            TestClass tc = new TestClass(100, a);
            TestClass tc1 = new TestClass(2056, a1);
            MyClass myClass = new MyClass(20, "unong", 200.1, tc, a);
            MyClass newMyClass = new MyClass(60, "unong1", 3.14159267, tc1, a1);

            Console.Write("myClass old value is:" +
                          "Age = " + myClass.Age + ";" +
                          "Name = " + myClass.Name + ";" +
                          "Data = " + myClass.Tc.Data + ";" +
                          "array[0] = " + myClass.ArrayTest[0] + ";" +
                          "Count = " + myClass.GetCount() + "\r\n");

            //(new CloneObject()).CopyProperties(myClass, newMyClass);
            try
            {
                CloneObject.CopyProperty(myClass, "Count", newMyClass.GetCount());
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            
            Console.Write("myClass new value is:" +
                          "Age = " + myClass.Age + ";" +
                          "Name = " + myClass.Name + ";" +
                          "Data = " + myClass.Tc.Data + ";" +
                          "array[0] = " + myClass.ArrayTest[0] + ";" +
                          "Count = " + myClass.GetCount() + "\r\n");
            Console.Read();
        }

        //static void Main(string[] args)
        //{
        //    int[] array = new int[1000000];
        //    MyClass newMyClass = new MyClass(60, "unong1", 3.14159267, new TestClass(23, array), array);
        //    MyClass myClass34 = new MyClass(60, "unong1", 3.14159267, new TestClass(23, array), array);
        //    Console.Write(myClass34.Age);
        //    int i = 0;
        //    for (; ; )
        //    {
        //        MyClass dd = new MyClass(3, "566", 56, new TestClass(56, array), array);
        //        (new CloneObject()).CopyProperties(dd, newMyClass);
        //        Console.Write("循环次数： " + (i++) + ";      "+ array[0].ToString() + "\r\n");
        //        System.Threading.Thread.Sleep(1);
        //    }
               
        //}

    }

    [Serializable]
    public class MyClass
    {
        public int Age { get; set; }
        public string Name { get; set; }
        private double Count { get; set; }
        public int[] ArrayTest { get; set; }
        public int[] ArrayTest1 { get; set; }
        public TestClass Tc { get; set; }
        public List<TestClass> Ltc { get; set; }

        public MyClass(int age, string name, double count, TestClass tc, int[] arrayTest)
        {
            ArrayTest = new int[10000000];
            ArrayTest1 = new int[10000000];
            Age = age;
            Name = name;
            Count = count;
            Tc = tc;
            ArrayTest = arrayTest;
        }

        public MyClass()
        {}

        public double GetCount()
        {
            return Count;
        }
    }

    public class TestClass
    {
        public int Data { get; set; }
        public int[] ArrayTest { get; set; }
        public int[] ArrayTest1 { get; set; }

        public TestClass(int data, int[] a)
        {
            Data = data;
            ArrayTest = a;
            ArrayTest1 = new int[10000000];
        }
    }

    public class CloneObject
    {
        public object CloneControl(object srcCtl)
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
                                object newChild = CloneControl(child);
                                object dstCollectionVal = dstPdc[i].GetValue(dstCtl);
                                var objects = (IList<object>) dstCollectionVal;
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

        /// <summary>
        /// 复制对象属性
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
            Type t = dstObject.GetType();
            PropertyInfo pI = t.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (pI.PropertyType != propertyValue.GetType())
            {
                throw new Exception("需要修改的属性类型不一致");
            }
            pI.SetValue(dstObject, propertyValue, null);

            //try
            //{
            //    PropertyDescriptorCollection dstPdc = TypeDescriptor.GetProperties(dstObject);

            //    PropertyDescriptor dstPd = dstPdc.Find(propertyName, true);
            //    if (dstPd == null)
            //    {
            //        throw new ArgumentNullException("需要修改的对象属性不存在");
            //    }

            //    if (dstPd.PropertyType != propertyValue.GetType())
            //    {
            //        throw new Exception("需要修改的属性类型不一致");
            //    }

            //    dstPd.SetValue(dstObject, propertyValue);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }


        public void CopyProperties(Object dst_ctl, object src_ctl)
        {
            try
            {
                //   clone   properties 
                PropertyDescriptorCollection src_pdc = TypeDescriptor.GetProperties(src_ctl);
                PropertyDescriptorCollection dst_pdc = TypeDescriptor.GetProperties(dst_ctl);

                for (int i = 0; i < src_pdc.Count; i++)
                {

                    if (src_pdc[i].Attributes.Contains(DesignerSerializationVisibilityAttribute.Content))
                    {

                        object collection_val = src_pdc[i].GetValue(src_ctl);
                        if ((collection_val is IList<object>) == true)
                        {
                            foreach (object child in (IList<object>)collection_val)
                            {
                                object new_child = this.CloneControl(child);
                                object dst_collection_val = dst_pdc[i].GetValue(dst_ctl);
                                ((IList<object>)dst_collection_val).Add(new_child);
                            }
                        }
                    }

                    else
                    {
                        dst_pdc[src_pdc[i].Name].SetValue(dst_ctl, src_pdc[i].GetValue(src_ctl));
                    }
                }

            }

            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

        } 

    }
}




/////////////////

namespace CloneSample
{
        //[Serializable]
        class CloneClass : ICloneable
        {
                int num;         //值类型

                public int Num //封装值字段
                {
                        get { return num; }
                        set { num = value; }
                }
                string str;    //string引用类型

                public string Str //封装引用字段
                {
                        get { return str; }
                        set { str = value; }
                }
                //数组引用类型
                public int[] intArr = new int[2];

                //实现接口的方法
                public Object Clone()
                {
                        //return this;                                        //返回同一个对象的引用
                    return this.MemberwiseClone();    //返回一个浅表副本
                    //return new CloneClass();                //返回一个深层副本
                        //{                                                                 //返回一个内存副本
                        //        MemoryStream stream = new MemoryStream();
                        //        BinaryFormatter formatter = new BinaryFormatter();
                        //        formatter.Serialize(stream, this);
                        //        stream.Position = 0;
                                 
                        //        return formatter.Deserialize(stream);
                                

                        //}

                }
        }
        //执行类
        //class ProgramRun
        //{
        //        public static void Main()
        //        {
        //                CloneClass cs = new CloneClass();
        //                //第一次给对象赋值
        //                cs.Num = 1;
        //                cs.Str = "A";
        //                cs.intArr[0] = 100;
        //               // CloneClass cs1 = cs.Clone() as CloneClass;
        //                CloneClass cs1 = new CloneClass();
        //                (new CloneObject()).CopyProperties(cs1, cs);
        //                Console.WriteLine("**************初始化*****************");
        //                Console.WriteLine("cs对象的值类型：{0}", cs.Num);
        //                Console.WriteLine("cs对象的string引用类型：{0}", cs.Str);
        //                Console.WriteLine("cs对象的数组类型:{0}", cs.intArr[0]);
        //                Console.WriteLine("-------------------------------------------------");
        //                Console.WriteLine("cs1对象的值类型：{0}", cs1.Num);
        //                Console.WriteLine("cs1对象的string引用类型：{0}", cs1.Str);
        //                Console.WriteLine("cs1对象的数组类型:{0}", cs1.intArr[0]);
        //                //第二次给原始对象复制
        //                cs.Num = 2;
        //                cs.Str = "B";
        //                cs.intArr[0] = 200;
        //                //现在我们看看cs和cs1两个对象的值
        //                Console.WriteLine("**************先将cs值改变后变化如下*****************");
        //                Console.WriteLine("cs对象的值类型：{0}", cs.Num);
        //                Console.WriteLine("cs对象的string引用类型：{0}", cs.Str);
        //                Console.WriteLine("cs对象的数组类型:{0}", cs.intArr[0]);
        //                Console.WriteLine("-------------------------------------------------" );
        //                Console.WriteLine("cs1对象的值类型：{0}", cs1.Num);
        //                Console.WriteLine("cs1对象的string引用类型：{0}", cs1.Str);
        //                Console.WriteLine("cs1对象的数组类型:{0}", cs1.intArr[0]);
        //                //现在我们给副本对象进行赋值看看原始对象的值
        //                cs1.Num = 3;
        //                cs1.Str = "C";
        //                cs1.intArr[0] = 300;
        //                Console.WriteLine("**************先将cs1值改变后变化如下*****************");
        //                Console.WriteLine("cs对象的值类型：{0}", cs.Num);
        //                Console.WriteLine("cs对象的string引用类型：{0}", cs.Str);
        //                Console.WriteLine("cs对象的数组类型:{0}", cs.intArr[0]);
        //                Console.WriteLine("-------------------------------------------------");
        //                Console.WriteLine("cs1对象的值类型：{0}", cs1.Num);
        //                Console.WriteLine("cs1对象的string引用类型：{0}", cs1.Str);
        //                Console.WriteLine("cs1对象的数组类型:{0}", cs1.intArr[0]);
        //                Console.WriteLine("Output Complete！Press Any Key To Continue.");
        //                Console.ReadKey();
        //        }
        //}
}