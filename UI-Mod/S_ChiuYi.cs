using System;
using Amazon.DynamoDBv2.Model;
using dc.ui;
using static dc.ui.OptionsSection;

namespace S_ChiuYi;

public class S_ChiuYi : S_Food
{
    public override OptionsSection.Indexes Index
    {
        get
        {
            return (OptionsSection.Indexes)17;
        }
    }

    public S_ChiuYi() : base()
    {

    }

}


