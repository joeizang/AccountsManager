using System;
namespace AccountManager.Domain.Abstractions
{
    interface IPayer
    {
        string Name { get; set; }
        int PayerId { get; set; }
    }
}
