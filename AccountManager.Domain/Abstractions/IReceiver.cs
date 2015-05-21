using System;
namespace AccountManager.Domain.Abstractions
{
    interface IReceiver
    {
        string Address { get; set; }
        string Name { get; set; }
        int ReceiverId { get; set; }
    }
}
