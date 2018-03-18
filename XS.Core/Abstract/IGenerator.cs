using System;

namespace XS.Core
{
    public interface IGenerator<TOutput>
    {
        TOutput Generate(Predicate<TOutput> validation);
    }
}