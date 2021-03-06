﻿# Mico.Shapes

Some object in `Micos`.

## Shape

### A Base Object

The `Micos` can have many kinds object.

Shape is the very basic object.

### Introduction

Shape should be a interface,you should use it by inheriting.

```C#
public class Life : Shape
{
	...
}
```

#### Event

On some events be occurred,some funciton will be run.

For example,the `Update` will be run when `Micos` update itself.

You can override this function.

```C#
public class Life : Shape
{
	public override void OnUpdate(object Unknown = null) 
	{
		Console.WriteLine("updating...");
	}
}
``` 

#### Export

You can render or write your shape.

#### Transform

A transofrm can describe a shape in the `Micos`.

##### Forward:
default is `(0,0,1)`.

You can think it is the local coordinate system's z-axis.

If you change forward,the rotate will be changed.
##### Rotate: 

We use `Quaternion` to rotate local coordinate system.

If you change rotate,the forward will be changed.

#### Collider

Collider is a object in `Mico.Objects`,you can use it to test collision.

If you want use it in a shape,you can follow this:

```C#
public Shape(){
    Collider = new XXXCollider(...);
}
```

If a shape intersects other shape,the function `OnCollide` will be called.

```C#
protected override void OnCollide(Shape target){
    ...
}
```