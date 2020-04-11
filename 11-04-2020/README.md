# Today I Learned

On the last day of the first week of the GoStack Bootcamp, we learned the basics
of TypeScript.

TypeScript adds typing and all the most recent features of the EcmaScript language,
but does not substitute javascript. The Node enviroment still can't 'understand'
TypeScript, so, when you code TypeScript, it needs to be converted to JavaScript.

It demands some getting used to, but it can improve the team's productivity, since
other developers don't need to guess variable types, and also adds functionality
to the IDE's IntelliSense.

On VSCode, specifically, the editor shows a '...' under imported packages. That means
the IntelliSense requires the '@types/<package>' to make sense of the packages' types.

Also, it is necessary to initialize a config file (yarn/npm tsc --init) and execute the
convertion of the TypeScript files to conventional JavaScript (yarn/npm tsc).

We can also define our own types using '.d.ts' files, which are acessed via import/export
and defining variables like 'var/let/const var: Type' | 'var/let/const var?: Type' (if
the variable is an optional parameter). Also, we can define 'interfaces' that represent
objects and it's properties types (interfaces also can contain interfaces).