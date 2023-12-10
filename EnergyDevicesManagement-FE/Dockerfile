# Using node:16-alpine base image
FROM node:16-alpine

# Set /app as the default work directory
WORKDIR /app

# Install Angular CLI globally
RUN npm install -g @angular/cli

# Copy package.json and package-lock.json to the working directory for package installation
COPY package.json package-lock.json ./

# Install project dependencies using npm
RUN npm install

# Copy all the project files to the working directory
COPY . .

# Expose the port of your application to bind with the host port (if needed)
EXPOSE 4200

# Run your app using the Angular CLI's ng serve command
CMD ["ng", "serve", "--host", "0.0.0.0"]
