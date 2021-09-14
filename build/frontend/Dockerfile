
ARG NODE_VERSION
FROM node:${NODE_VERSION}

WORKDIR /app

COPY ./Frontend/ ./

RUN npm install -g @angular/cli @angular-devkit/build-angular && npm install

CMD ng serve --host 0.0.0.0
