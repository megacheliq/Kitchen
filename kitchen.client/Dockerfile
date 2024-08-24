FROM node:18-alpine AS build

WORKDIR /app

COPY . .

RUN npm install -g typescript vite

RUN npm install

RUN npm run build

FROM nginx:stable-alpine

RUN rm /etc/nginx/conf.d/default.conf

COPY nginx.conf /etc/nginx/conf.d/

COPY --from=build /app/dist /var/www/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]