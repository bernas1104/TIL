# Today I Learned

Today, on the GoStack Bootcamp, we'll be learning about how to deploy our React.js
applications. They showed a simpler way for [smaller projects](http://netlify.com/)
and more robust [service](https://cloud.google.com/storage).

## Prepare to Deploy

As we did with the back-end of the application, we need to prepare our front-end
for deployment. Since we create the application using the "create-react-app",
there's little we need to do.

Firts, we need to change the "baseURL" of the axios instance. We can create a
.env and a .env.example to contain the environment variables of our application.
The "baseURL" property of the axios instance will receive a environment variable
that will point to our back-end address.

We can't upload this to our repository (.env) and will upload only the example
file, as was the case with the back-end.

## Netlify

On the Netlify website, we can create/login with our Github account. To create
a site, we select a Git repository (Hub, Lab, Bucket), the build command, folder
and the environment variables. After that, just click deploy.

Netlify already configures the CI/CD with Github. There are preview URLs for
Pull Requests on branches and lots more.

It is recommended that we use a "api.mydomain.com" (sub-domain) to access the
back-end and "mydomain.com" to access the front-end.
