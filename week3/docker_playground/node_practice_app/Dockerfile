# 1. Official minimal node.js runtime
FROM node:20-alpine

# 2. Set the working directory
WORKDIR /usr/src/app

# 3. Copy package manifests first for layer caching
COPY package*.json ./

# 4. Install production dependencies
RUN npm ci --omit=dev

# 5. Copy the rest of the application code
COPY . .

# 6. Switch to unprivilaged user included in alpine image
USER node

# 7. Document the listening port
EXPOSE 3000

# 8. Start command
CMD ["npm", "start"]