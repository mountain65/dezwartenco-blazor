There's quite a few things said on the
[Builder Pattern](http://en.wikipedia.org/wiki/Builder_pattern)
So I assume that you are sure you need a builder and are looking for a nice
implementation.

The implementations I found (like
[Cameron McKay's](http://cdmckay.org/blog/2009/07/03/joshua-blochs-builder-pattern-in-csharp/)
or [Nat Pryce's](http://www.natpryce.com/articles/000714.html)
follow more or less the same pattern (of course): a builder class with
methods for each part of the class that it needs to build. Code that looks
very repetitive, while you were looking for a pattern to simplify your code
in the first place!The result in your Director however is pretty neat. If
you go for a fluent interface you might get something like
  

``` csharp
  Car car = new CarBuilder()  
    .WithDoors(4)
    .WithColor("red")
    .WithABS(true)
    .Build();
```

(with a special thanks to
[Tobiask](http://stackoverflow.com/a/696097/68940))

But what if you could make the building of objects generic and reusable,
with some sort of configuration to tell the builder what it needs to make?
Fortunately I have this brilliant colleague @JelleHissink who wrote this code in 3 minutes from the top of his head. And then
rambled on for 5 more minutes on improvements that I totally did not grasp.

### A generic builder

```csharp
public class Builder<T> where T : new()  
{
    IList<Action<T>> actions = new List<Action<T>>();
    public T Build ()    
    {
        var built = new T ();
        foreach (var action in actions) 
        {
            action (built);
        }
        return built;
    }

    public Builder<T> With(Action<T> with)
    {
        actions.Add (with);
        return this;    
    }
}
```
