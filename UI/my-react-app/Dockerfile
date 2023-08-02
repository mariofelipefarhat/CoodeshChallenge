FROM node:alpine AS build

WORKDIR /client

COPY package.json package.json
COPY package-lock.json package-lock.json

RUN npm ci --production

COPY . .

RUN npm run build

FROM nginx:1.12-alpine as prod

COPY --from=build /code/build /usr/share/nginx/html

EXPOSE 3000
# ENV PATH /app/node_modules/.bin:$PATH



CMD ["nginx", "-g", "daemon off;"]