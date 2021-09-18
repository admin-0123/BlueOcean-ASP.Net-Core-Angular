
ARG NODE_VERSION
FROM node:${NODE_VERSION}

WORKDIR /app

COPY ./Frontend/package.json package.json
COPY ./Frontend/package-lock.json package-lock.json

RUN npm install -g @angular/cli @angular-devkit/build-angular && npm install

CMD ng serve --host 0.0.0.0 --port 4200 --open --disable-host-check
