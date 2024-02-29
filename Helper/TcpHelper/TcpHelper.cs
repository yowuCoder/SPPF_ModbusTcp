// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Net;
// using System.Net.Sockets;
// using System.Text;
// using System.Threading;
// using System.Threading.Tasks;
// using Microsoft.Extensions.Options;
// using NetCoreServer;
// using Serilog;

// namespace kl_modbus_core.Helper.TcpHelper
// {
//     public class MyTcpClient : NetCoreServer.TcpClient
//     {

//         /// <summary>
//         /// 連接成功事件 item1:是否連接成功
//         /// </summary>
//         public event Action<bool> OnConnect;
//         /// <summary>
//         /// 接收通知事件 item1:數據
//         /// </summary>
//         public event Action<byte[]> OnReceive;
//         /// <summary>
//         /// 切割byte內容
//         /// </summary>
//         public Func<List<byte>, Tuple<List<byte>, List<Byte[]>>> SliceByteFunc;
//         /// <summary>
//         /// 已發送通知事件 item1:長度
//         /// </summary>
//         public event Action<long, long> OnSend;
//         /// <summary>
//         /// 斷開連接通知事件
//         /// </summary>
//         public event Action OnClose;
//         /// <summary>
//         /// 接收到的数据缓存
//         /// </summary>
//       //  private List<byte> queue = new List<byte>();
//         private bool _stop;
//         public MyTcpClient(IPAddress address, int port) : base(address, port)
//         {
//             if (SliceByteFunc == null)
//             {
//                 SliceByteFunc = CommSlicingByteFunc;
//             }
//         }

//         private Tuple<List<byte>, List<byte[]>> CommSlicingByteFunc(List<byte> arg)
//         {
//             throw new NotImplementedException();
//         }

//         // private readonly SocketSettingOption _socketSetting;
//         // public MyTcpClient(IPAddress address, int port
//         //     , IOptions<SocketSettingOption> options) : base(address, port)
//         // {
//         //     if (SliceByteFunc == null)
//         //     {
//         //         SliceByteFunc = CommSlicingByteFunc;
//         //     }
//         //     _socketSetting = options.Value;
//         // }

//         public void DisconnectAndStop()
//         {
//             _stop = true;
//             DisconnectAsync();
//             while (IsConnected)
//                 Thread.Yield();
//         }

//         protected override void OnConnected()
//         {
//             Log.Information($"Chat TCP client connected a new session with Id {Id}");
//             OnConnect(this.IsConnected);
//         }

//         protected override void OnDisconnected()
//         {
//             Log.Information($"Chat TCP client disconnected a session with Id {Id}");
//             OnClose();

//             // Wait for a while...
//             Thread.Sleep(1000);

//             // Try to connect again
//             if (!_stop)
//                 ConnectAsync();
//         }

//         // protected override void OnReceived(byte[] buffer, long offset, long size)
//         // {
//         //     try
//         //     {
//         //         if (OnReceive != null)
//         //         {
//         //             Log.Information($"[Buffer] {buffer.Length};[Offset] {offset};[Size] {size}");
//         //             var dataBuffer = ByteExtensions.GetSubByte(buffer, (int)offset, (int)size);
//         //             Log.Information($"[TcpServer_eventactionReceive:data]: {CoreHelper.Encoding.GetString(dataBuffer)}");
//         //             queue.AddRange(dataBuffer);
//         //             Log.Information($"[TcpServer_eventactionReceive:queue]: {CoreHelper.Encoding.GetString(queue.ToArray())}");
//         //             lock (queue)
//         //             {
//         //                 var sliceResult = SliceByteFunc(queue);
//         //                 queue = sliceResult.Item1;
//         //                 foreach (var item in sliceResult.Item2)
//         //                 {
//         //                     OnReceive(item);
//         //                 }
//         //             }
//         //         }
//         //     }
//         //     catch (Exception ex)
//         //     {
//         //         Log.Information($"[TcpServer_eventactionReceive error]: {ex}");
//         //     }
//         // }

//         protected override void OnError(SocketError error)
//         {
//             Log.Information($"Chat TCP client caught an error with code {error}");
//         }

//         protected override void OnSent(long sent, long pending)
//         {
//             OnSend(sent, pending);
//             base.OnSent(sent, pending);
//         }
//     }

// }
