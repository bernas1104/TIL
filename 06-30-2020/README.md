# Today I Learned

On the last (maybe?) module of the GoStack Bootcamp, we learned about deploying
our applications.

## Preparing the Code

So, the first step to deploy our applications is to generate the build. If we
take a look at our package.json file, there's a script "build" that runs the "tsc".
Unfortunately, the "tsc" command is very slow and, also, it does not understand
(out of the box) the @paths we used on our code.

To fix this, we will use the Babel package. First, we need to install some new
packages as development dependencies:

> yarn add -D @babel/cli @babel/core @babel/node @babel/preset-env @babel/preset-typscript babel-plugin-module-resolver babel-plugin-transform-typescript-metadata @babel/plugin-proposal-decorators
> @babel/plugin-proposal-class-properties

After the installation, we need to create a "babel.config.js" file on our root
folder. There we'll expose our configs:

```js
  presets: [
    ['@babel/preset-env', { targets: { node: 'current' } }],
    '@babel/preset-typescript',
  ],
  plugins: [
    ['module-resolver', {
      alias: {
        "@modules": "./src/modules",
        "@config": "./src/config",
        "@shared": "./src/shared",
      }
    }],
    "babel-plugin-transform-typescript-metadata",
    ["@babel/plugin-proposal-decorators", { "legacy": true }],
    ["@babel/plugin-proposal-class-properties", { "loose": true }],
  ],
```

These define the @babel basic configurations (preset) and to what node version
we should transpile the code ('current'). Also, tells Babel how to deal with our
@paths.

After these configurations, we need to change our build script inside package.json:

```json
{
  "scripts": {
    "build": "babel src --extensions \".js,.ts\" --out-dir dist --copy-files"
  }
}
```

## Github Repository

After creating a repository, we must check that the ".env" and "ormconfig.json"
files are listed on the ".gitignore" file to ensure that they won't be uploaded
to the repository. This is because our server (DigitalOcean, Heroku, AmazonAWS,
etc) will provide these files and if we upload them to git, it is a security risk.
Also, the dist folder won't be uploaded.

## Preparing the Server

Not much to say here. Every service will be different, so I don't think there's
much point on explaining how deploy a server on any of the possible services.
Just choose one, read the documentation on how to begin and that's it.

It's interesting to create a SSH Key to access the server from your terminal.

After installing the server, you should update it. It'll probably take a long time,
but it's very important to do so.

You should create a new user - since Root has permission to do anything on the
server. The SSH Key configuration must be applied to the new user.

After this, we need to install the LTS version of Node.js, since we'll need it to
run our application and the server probably won't have it installed by default.

For a private Git repository, we need to create a SSH Key to share between the
server and the repository, so we can clone the code in a more secure way.

> ssh-keygen
> cd ~/.ssh
> cat id_rsa.pub

Then, we copy the SSH Key and add it to the Git repository. Now the server can
access the repository. This will be used only in the initial deploy, since the
CI/CD part will be handled by Github (or any other service), i.e., this process
will be performed by the Git repository.

## Preparing Docker

If your application is using any docker images, you need to install them on the
server.

On the video, was suggested to use the Bitnami PostgreSQL Docker image. It's
recommended that you change the default port (5432) to a random port. This is
because, if someone tries to hack your server, the first ports they will try to
access are the default ones.
[Bitnami PostgreSQL Docker](https://github.com/bitnami/bitnami-docker-postgresql)

For the MongoDB Docker Image, Bitnami also provides a image, which the video also
suggests. Changing the ports is important here too.
[Bitnami MongoDB Docker](https://github.com/bitnami/bitnami-docker-mongodb)

For the Redis Docker Image, there's also a option from Bitnami, which the video
suggests.
[Bitnami Redis Docker](https://github.com/bitnami/bitnami-docker-redis)

## NGINx Reverse Proxy

While developing our application, we were using the port 3333, but, by default,
all web applications use the 80 port. When you type any URL on your browser, it
is implied that, at the end, there is a :80. We could change our server port from
3333 to 80, but if we were to run multiple applications on a server, this could
be a problem. Instead, we'll use the proxy mechanism to redirect all the traffic
from the 80 to the 3333 port.

After accessing the server, run:

> sudo apt install nginx

After that, we need to allow access from the 80 port, run:

> sudo ufw allow 80

With this, you already can access the NGINx webpage through your browser, using
your server IP address. What we need to do now is create a new file on the
"sites-available" folder on the NGINx folder (/etc/nginx/sites-available). Digital
Ocean has a [tutorial](https://www.digitalocean.com/community/tutorials/how-to-set-up-a-node-js-application-for-production-on-ubuntu-18-04)
on how to configure the NGINx Reverse Proxy.

After setting the Proxy, we need to create a symbolic link to the file we just
created on the "sites-enabled" folder, and we can delete de "default" link on the
folder. Now run:

> sudo nginx -t

To make sure everything is OK. Then:

> sudo nginx systemctl restart nginx

To restart the server and apply the changes.

## Keeping the application live

First, we need to tell Docker how to behave if the server restarts. To do this,
run:

> sudo docker update --restart=unless-stopped <image_id>

This will ensure that the images are restarted if something happens, unless we
manually stop it.

Now, to ensure that our application will restart if something happens, we need a
library called PM2. To install it, run:

> sudo npm install pm2 -g

We need to run our applcation using the PM2 library. To do this, run:

> pm2 start <location_of_your_server_file> --name <name_of_your_application>

And that's it. Now, if the application shuts down for any reason, it will be restarted
by the PM2. The command "pm2 list" shows all the applications PM2 is managing.

Last, we need to ensure that the PM2 will run every time the server gets restarted.
So, run:

> pm2 startup systemd

It will generate a command. We need to copy that and paste it on the terminal.

Some interesting commands of the PM2 includes "logs" and "monit", to show application
logs and monitor the applications respectively.

## SSL Certification

Apparently there's a service that provides free SSL for websites. It's called
[Certbot](https://certbot.eff.org). All the steps are provided on the website.
We also need to allow the 443 port. Run:

> sudo ufw allow 443

And you're done.

## CI/CD

First, we need to create Secrets on our Github repository.

- One for the server IP address;
- One for the server user;
- One for the server port;
- One for the SSH Key

We must add the generated SSH Key as a valid key for the server (~/.ssh/authorized_keys)
and add the key at the end of the file. Then, we must copy the secret key to the
SSH Key secret on github.

After confuguring the secrets, we need to create our Github Action. Github has a
lot of pre-made options, including for Node.js. It's nice to comment the steps
of your actions to make them clearer. To transfer the files to the server, there's
[this](https://github.com/appleboy/scp-action) action. Also, we can add a cache
of the node_modules dependencies to make the deploy faster and avoid sending the
node_modules folder for the same reason.
