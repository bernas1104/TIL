# TODAY I Learned

This week, we started the fourth module of the Codenation program. We started learning
the basics of S.O.L.I.D, Design Patterns, TDD and Clean Code. This week we'll be
focusing more on theoretical concepts than code.

## S.O.L.I.D

So, S.O.L.I.D is an acronym for five principles that guide Object Oriented Programming.
It is said that, if you're not following the S.O.L.I.D principles, you're not using
Object Oriented Programming.

### Single Reposability Principle (S)

In his book, Clean Architecture, Robert C. Martin says that the Single Reposability
Principle is probably the least comprehended. When developers hear the name, they
think "modules must do only one thing". This is wrong.

There **is** a principle that tells us that, but it is applied to functions, not
modules. The SRP principle can be describe as:

*"A module must have one, and only one, reason to change"*

By *reason*, he means a user or stakeholder. We can reformulate the principle to:

*"A module must be responsible for one, and only one, user or stakeholder"*

Unfortunately, "user" and "stakeholder" are not the correct expressions. Usually,
there are more than one user or stakeholder demanding system changes. So, the
correct word would be "actor", for a group of users or stakeholders.

*"A module must be responsible for one, and only one, actor"*

### Open Closed Principle (O)

Created by Bertrand Meyer, it says:

*"A software artifact must be open for extension, but closed for modification."*

The ideia is that when the requisites change, we should extend the behaviour of
the modules, by adding new code, and not changing the old code that works. To achieve
this, we need to use abstractions and polymorphism.

### Liskov Substitution Principle (L)

The definition of the Liskov Substitution Principle is:

*"If, for each o1 object of type S, there's a o2 object of type T, such that,
for all P programs defined by T, the behaviour of P does not change when o1 is
substituted for o2, then S is a subtype of T."*

To better understand the principle, take this example. Imagine a "License" class.
This class has a "calcFee()" method, called by a "Billing" application. There are
two subtypes of "Licenses": PersonalLicense and BusinessLicense. We can define
License as an interface, which will be implemented by the subtypes. By doing that,
Billing does not depende, in any way, of the License subtypes, since they are both
replaceble by the "License" type.

### Interface Segregation Principle (I)

The idea of the Interface Segregation Principle is to avoid dependency of something
that contains unnecessary items. Instead of having big interfaces, it is best to
create multiple smaller ones that are specific to each "client".

### Dependency Inversion Principle (D)

To create more flexible systems, the source code dependencies must refer to abstractions
and not to concrete implementations. To help achieve this, we must follow four rules:

1. Don't refer to concrete volatile classes;
2. Don't derive from concrete volatile classes;
3. Don't override concrete functions;
4. Never mention the name of something that is concrete and volatile.

Also, there is the Factories pattern that helps with avoiding the dependency on concrete
implementations. It defines two interfaces: one used to make a request for an instance
of the class that has the concrete implementation and another to receive the concrete
implementation and make it available to the application.
