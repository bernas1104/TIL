# Today I Learned

On the fourth module of the GoStack Bootcamp, we finished the back-end of the GoBarber
application. On the last classes of the module, we learned about Class Transformer lib,
an introduction on caching with Redis and a deeper dive on SQL Relations.

## Class Transformer

In some cases, we need to ommit or expose some properties of an entity. Instead of
deleting or adding this information everytime, we can use the [class transformer](https://github.com/typestack/class-transformer)
lib to achieve theses goals.

With it, we can define - right on the entity file - which information we want to
ommit (@Exclude) or expose (@Expose({ name: 'property_name' })). Then, all we need
to do is call a method to apply the properties changes on the object (classToClass,
for example).

## Cache (with Redis)

The NoSQL Database Redis is a simple way to store key-values information. To use it,
we need to install the Redis database (obviously) and the [ioredis](https://github.com/luin/ioredis).
The idea is to store information that might be accessed very often. Every time that
information needs to be read, instead of going straight for the regular database,
we check the cache first and if the information is there, we returned. This greatly
improves the time of response. On a simple example, the regular read time of the application
was 25ms, but with the cache access, that time droped to 5ms.

We need to always keep the cache valid. Sometimes we will need to delete every key that
follows a pattern. To make this faster, we can use the "pipeline" function. It is a
similar method to create an array of Promises and process them all at once.


## SQL Relations

ManyToOne/OneToMany relations define that for each instance of A, there's multiple instances
of B, but B contains only one instance of A. The OneToMany relations does not exist
without ManyToOne relations.

On the coding of the entities, we always need to define a property that will receive the
instance of the relation.

When we have OneToMany entities, usage of the "eager: true" flag will ensure that the reading
of the data automatically returns the related instance. This will happen all the time. If
you only need this to happen in some specific database queries, you can define the option
"relations" .
