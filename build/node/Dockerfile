
ARG NODE_VERSION
FROM node:${NODE_VERSION}
ENV NODE_ENV=production

WORKDIR /app

COPY ./Node/package.json package.json
COPY ./Node/package-lock.json package-lock.json

RUN npm install --production

CMD [ "node", "server.js" ]
