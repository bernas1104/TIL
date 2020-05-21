# Today I Learned

## Feature Mapping

Today our instructor at the GoStack Bootcamp gave us a tip on how to keep track
of the features you need to develop. To map the features, you'll need to be guided
by the applications screens (if you have them) or by talking to the client.

Every feature can be broken into two categories: micro and macro.

Every micro feature is contained within a macro feature, and can be further divided
into three sub-categories: functional requirements, non-functional requirements and
business rules.

- Functional Requirements: are the functionalities within a macro feature. For example,
in a "Password Recovery" feature, the user can recover his/her password using the
account e-mail.
- Non-Functional Requirements: it's everything not directly related to the business
rules, i.e., the use of a specific e-mail library or a specific development framework.
- Business Rules: are the conditions to the functional requirements. For example,
in a "Password Recovery" scenario, the user has X hours to complete the process or
he/she has to start again.

Basically, functional requirements + business rules tells us WHAT the user must be
capable to do, while non-functional requirements tells HOW these features will be
accomplished (libs, algorithms, frameworks, databases, etc...)

## Test Driven Development (TDD)

If you are using the TDD method, you're not supposed to write ALL the tests for all
the features to, only after this, start writing the features themselves, but write
a test for some aspect of the feature and write the least amount of code to make that
test pass and so on. It's an iterative process!

### Mocks

Previously, I had a completely wrong understing about mocks. On Jest, mocks are used
to check when a given function is called and, if you want, change the results given
by the function. By doing this, you have complete control over the function. To do this,
Jest gives you an array of methods: mock, mockImplementation, mockImplementaionOnce...

### What does TDD assure?

It doesn't ensure that the system is functional, but ensures that you business rules
are intact. After doing all the unit tests with TDD, you can proceed to integration
testing, which will ensure that the system, as a whole, is functional.
