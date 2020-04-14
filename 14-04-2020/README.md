# Today I Learned

Today I started the second week of the GoStack Bootcamp. This  week will focus on
the back-end of the JavaScript application that the students will had build at
the end of the Bootcamp.

Before start the code, we explored ways to ensure and automate code structures and
patterns. To accomplish that, we learned about [ESLint](https://eslint.org/) and
[Prettier](https://prettier.io/). Also, to make it easier to develop with TypeScript
we learned about "ts-node-server", which is a way to execute the project without
the need to produce a build or restart the server manually to see the changes made
on the code.

To make the project performe better, while in development, we can pass some flags
to the "ts-node-server", such as:

- "--transpileOnly": it ensures that the "ts-node-server" won't check for errors
on the code (since the IDE will be checking these), and will make the project run
faster;
- "--ignore-watch node_modules": it tells the "ts-node-server" to ignore the
"node_modules" folder, which is not the developers responsability;
- "--inspect": it prepares the server to be debugged later

To begin with the code patterns, we must install the (in my case) VSCode extensions
editorConfig and ESLint. Then, we need to add the ESLint module to our project.
Now we need to create the editorConfig file. To do that, we simply right-click the
root folder on VSCode and select "Generate editorConfig". This will create a editorConfig
file with all the configuration. To create the ESLint config file, simply run:

> yarn eslint --init

To make ESLint understand "import/export" with TypeScript, we need to edit the .eslintrc.json
file.

The "Prettier" tool is responsible to automate the patterns on the project's code.
It integrates with the ESLint tool. To accomplish this, we just need to add some
information to the .eslintrc.json file.

All the configurations can be checked at [this](https://github.com/bernas1104/gostack/tree/master/semana-ii/primeiro-projeto-node)
repository

The last thing learned on the beginning of this week was the use of the VSCode
debugger tool. We can create a new configuration file on the VSCode menu and then
we need to change some things:

- request: attach. This will make so that we don't launch a new instance when we
debug the code. The debugger tool will attach to the server (--inspect flag) and
we will be able to debug it.
- protocol: inspector. Same as described above!
- name: < anything >. Can be anything you want.
- delete the "program" option. It won't be necessary since we will be 'attaching'.

With all theses things done, we can ensure that the code will be following a pretty
good structure and pattern, which will make it easier for a team to work on. It's
awesome!
