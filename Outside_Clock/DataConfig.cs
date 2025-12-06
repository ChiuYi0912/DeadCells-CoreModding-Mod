using System;
using dc;
using ModCore.Events.Interfaces.Game;
using ModCore.Storage;
using Serilog;

namespace Outside_Clock;

public struct FastBoolSerializer : IHxbitSerializable<bool>, IOnGameExit
{
    public bool Value;

    public FastBoolSerializer()
    {
    }

    public bool GetData() => Value;



    public void SetData(bool data) => Value = data;

    // 运算符重载，方便使用
    public static implicit operator bool(FastBoolSerializer serializer) => serializer.Value;


    public void OnGameExit()
    {

    }
}