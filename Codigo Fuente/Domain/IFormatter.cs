using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IFormatter
    {
        string GetName();
        List<Accomodation> Upload(List<SourceParameter> sourceParameters);
    }
}
