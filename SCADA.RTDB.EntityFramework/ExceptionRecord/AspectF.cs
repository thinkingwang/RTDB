using System;
using System.Diagnostics;

namespace SCADA.RTDB.EntityFramework.ExceptionRecord
{
    /// <summary>
    ///AspectF 的摘要说明
    /// </summary>
    public class AspectF
    {
        internal Action<Action> Chain = null;

        internal Delegate WorkDelegate;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="work"></param>
        [DebuggerStepThrough]
        public void Do(Action work)
        {
            if (Chain==null)
            {
                work();
            }
            else
            {
                Chain(work);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TReturnType"></typeparam>
        /// <param name="work"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public TReturnType Return<TReturnType>(Func<TReturnType> work)
        {
            WorkDelegate = work;

            if (Chain==null)
            {
                return work();
            }
            TReturnType returnValue = default(TReturnType);
            Chain(() =>
                {
                    var workDelegate = WorkDelegate as Func<TReturnType>;
                    if (workDelegate != null) returnValue = workDelegate();
                });
            return returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        public static AspectF Define
        {
            [DebuggerStepThrough]
            get
            {
                return new AspectF();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newAspectDelegate"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public AspectF Combine(Action<Action> newAspectDelegate)
        {
            if (Chain==null)
            {
                Chain = newAspectDelegate;
            }
            else
            {
                Action<Action> existingChain = Chain;
                Action<Action> callAnother = work =>
                                             existingChain(() => newAspectDelegate(work));
                Chain = callAnother;
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="retryDuration"></param>
        /// <param name="retryCount"></param>
        /// <param name="errorHandler"></param>
        /// <param name="retryFaild"></param>
        /// <param name="work"></param>
        public static void Retry(int retryDuration, int retryCount, Action<Exception> errorHandler, Action retryFaild, Action work)
        {
            do
            {
                try
                {
                    work();
                }
                catch (Exception ex)
                {
                    errorHandler(ex);
                    System.Threading.Thread.Sleep(retryDuration);
                    throw;
                }
            } while (retryCount-- > 0);

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class AspectExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        [DebuggerStepThrough]
        public static void DoNothing()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whatever"></param>
        [DebuggerStepThrough]
        public static void DoNothing(params object[] whatever)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aspect"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static AspectF Delay(this AspectF aspect, int milliseconds)
        {
            return aspect.Combine(work =>
                {
                    System.Threading.Thread.Sleep(milliseconds);
                    work();
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aspect"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [DebuggerStepThrough]
        public static AspectF MustBeNonNull(this AspectF aspect,params object[] args)
        {
            return aspect.Combine(work =>
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        object arg = args[i];
                        if (arg==null)
                        {
                            throw new ArgumentException(string.Format("Parameter at index {0} is null", i));
                        }
                    }
                    work();
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aspect"></param>
        /// <param name="args"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static AspectF MustBeNonDefault<T>(this AspectF aspect, params T[] args) where T : IComparable
        {
            return aspect.Combine(work =>
                {
                    T defaultvalue = default(T);
                    for (int i = 0; i < args.Length; i++)
                    {
                        T arg = args[i];
                        if (arg==null||arg.Equals(defaultvalue))
                        {
                            throw new ArgumentException(string.Format("Parameter at index {0} is null", i));
                        }
                    }
                    work();
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aspect"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static AspectF WhenTrue(this AspectF aspect, params Func<bool>[] conditions)
        {
            return aspect.Combine(work =>
                {
                    foreach (Func<bool> condition in conditions)
                    {
                        if (!condition())
                        {
                            return;
                        }
                    }
                    work();
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aspect"></param>
        /// <param name="completeCallback"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static AspectF RunAsync(this AspectF aspect, Action completeCallback)
        {
            return aspect.Combine(work => work.BeginInvoke(asyncresult =>
                {
                    work.EndInvoke(asyncresult); completeCallback();
                }, null));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aspect"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static AspectF RunAsync(this AspectF aspect)
        {
            return aspect.Combine(work => work.BeginInvoke(work.EndInvoke, null));
        }
    }
}