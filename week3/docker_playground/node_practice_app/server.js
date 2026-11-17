const express = require("express");
const { createClient } = require("redis");

const app = express();
const PORT = 3000;

// Read redis host from environment variable
const redisHost = process.env.REDIS_HOST || "redis";
const redisPort = process.env.REDIS_PORT || 6379;

const client = createClient({
  url: `redis://${redisHost}:${redisPort}`,
});

client.on("error", (err) => console.log("redis client error", err));

async function startServer() {
  await client.connect();
  console.log(`Connected to Redis at ${redisHost}:${redisPort}`);

  app.get("/", async (req, res) => {
    try {
      const count = await client.incr("page_hits");
      res.send(`
                <h1>Hello Docker Learner (Node.js Edition)!</h1> <p>This page has been visited <strong>${count}</strong> times.</p>
                `);
    } catch (err) {
      console.error(err);
      res.status(500).send("Error incrementing counter");
    }
  });

  app.listen(PORT, "0.0.0.0", () => {
    console.log(`Server listening on port ${PORT}`);
  });
}

startServer();
