FROM node:latest as build
WORKDIR /app

RUN npm install -g @angular/cli

COPY ./package.json .
RUN npm install
COPY . .
RUN ng build --configuration development

FROM nginx as runtime
COPY ./.nginx.conf /etc/nginx/conf.d/default.conf

RUN rm -rf /usr/share/nginx/html/*

COPY --from=build /app/dist/blog-cms /usr/share/nginx/html

CMD ["nginx", "-g", "daemon off;"]