using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace SCADA.RTDB.Transport
{
    public abstract class TransportBase : IDisposable
    {
        private int _waitToRetryMilliseconds;
        private IStreamResource _streamResource;

        #region 属性

        /// <summary>
        /// 重试次数
        /// </summary>
        public int Retries { get; set; }

        /// <summary>
        /// 重试间隔时间
        /// </summary>
        public int WaitToRetryMilliseconds
        {
            get { return _waitToRetryMilliseconds; }
            set
            {
                if (value < 0)
                    throw new ArgumentException(Resource1.WaitRetryGreaterThanZero);

                _waitToRetryMilliseconds = value;
            }
        }

        /// <summary>
        /// 读超时
        /// </summary>	
        public int ReadTimeout
        {
            get { return StreamResource.ReadTimeout; }
            set { StreamResource.ReadTimeout = value; }
        }

        /// <summary>
        /// 写超时
        /// </summary>
        public int WriteTimeout
        {
            get { return StreamResource.WriteTimeout; }
            set { StreamResource.WriteTimeout = value; }
        }

        /// <summary>
        /// 资源数据流
        /// </summary>
        internal IStreamResource StreamResource
        {
            get { return _streamResource; }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// This constructor is called by the NullTransport.
        /// </summary>
        internal TransportBase()
        {
        }

        internal TransportBase(IStreamResource streamResource)
        {
            Debug.Assert(streamResource != null, "Argument streamResource cannot be null.");

            _streamResource = streamResource;
        }

        #endregion


        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //internal virtual T UnicastMessage<T>(byte[] message) where T : IModbusMessage, new()
        //{
        //    IModbusMessage response = null;
        //    int attempt = 1;
        //    bool readAgain;
        //    bool success = false;

        //    do
        //    {
        //        try
        //        {
        //            lock (_syncLock)
        //            {
        //                Write(message);

        //                do
        //                {
        //                    readAgain = false;
        //                    response = ReadResponse<T>();

        //                    var exceptionResponse = response as SlaveExceptionResponse;
        //                    if (exceptionResponse != null)
        //                    {
        //                        // if SlaveExceptionCode == ACKNOWLEDGE we retry reading the response without resubmitting request
        //                        if (readAgain = exceptionResponse.SlaveExceptionCode == Modbus.Acknowledge)
        //                        {
        //                            _logger.InfoFormat("Received ACKNOWLEDGE slave exception response, waiting {0} milliseconds and retrying to read response.", _waitToRetryMilliseconds);
        //                            Thread.Sleep(WaitToRetryMilliseconds);
        //                        }
        //                        else
        //                        {
        //                            throw new SlaveException(exceptionResponse);
        //                        }
        //                    }
        //                } while (readAgain);
        //            }

        //            ValidateResponse(message, response);
        //            success = true;
        //        }
        //        catch (SlaveException se)
        //        {
        //            if (se.SlaveExceptionCode != Modbus.SlaveDeviceBusy)
        //                throw;

        //            _logger.InfoFormat("Received SLAVE_DEVICE_BUSY exception response, waiting {0} milliseconds and resubmitting request.", _waitToRetryMilliseconds);
        //            Thread.Sleep(WaitToRetryMilliseconds);
        //        }
        //        catch (Exception e)
        //        {
        //            if (e is FormatException ||
        //                e is NotImplementedException ||
        //                e is TimeoutException ||
        //                e is IOException)
        //            {
        //                _logger.WarnFormat("{0}, {1} retries remaining - {2}", e.GetType().Name, _retries - attempt + 1, e);

        //                if (attempt++ > _retries)
        //                    throw;
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //    } while (!success);

        //    return (T)response;
        //}

        //internal virtual IModbusMessage CreateResponse<T>(byte[] frame) where T : IModbusMessage, new()
        //{
        //    byte functionCode = frame[1];
        //    IModbusMessage response;

        //    // check for slave exception response else create message from frame
        //    if (functionCode > Modbus.ExceptionOffset)
        //        response = ModbusMessageFactory.CreateModbusMessage<SlaveExceptionResponse>(frame);
        //    else
        //        response = ModbusMessageFactory.CreateModbusMessage<T>(frame);

        //    return response;
        //}

        //internal void ValidateResponse(IModbusMessage request, IModbusMessage response)
        //{
        //    // always check the function code and slave address, regardless of transport protocol
        //    if (request.FunctionCode != response.FunctionCode)
        //        throw new IOException(String.Format(CultureInfo.InvariantCulture, "Received response with unexpected Function Code. Expected {0}, received {1}.", request.FunctionCode, response.FunctionCode));

        //    if (request.SlaveAddress != response.SlaveAddress)
        //        throw new IOException(String.Format(CultureInfo.InvariantCulture, "Response slave address does not match request. Expected {0}, received {1}.", response.SlaveAddress, request.SlaveAddress));

        //    // message specific validation
        //    request.Is<IModbusRequest>(req => req.ValidateResponse(response));

        //    OnValidateResponse(request, response);
        //}

        ///// <summary>
        ///// Provide hook to do transport level message validation.
        ///// </summary>
        //internal abstract void OnValidateResponse(IModbusMessage request, IModbusMessage response);

        //internal abstract byte[] ReadRequest();

        //internal abstract IModbusMessage ReadResponse<T>() where T : IModbusMessage, new();

        //internal abstract byte[] BuildMessageFrame(IModbusMessage message);

        //internal abstract void Write(IModbusMessage message);

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            //if (disposing)
            //    DisposableUtility.Dispose(ref _streamResource);
        }

    }
}
