FROM nginx:stable

ARG BUILD_DIR=Release
ADD $BUILD_DIR /usr/share/nginx/html

EXPOSE 80
