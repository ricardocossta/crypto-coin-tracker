## CryptoCoinTracker:

### Features

CryptoCoinTracker is a study project focused on learning and practicing the use of RabbitMQ for communication between microservices, as well as working with MongoDB for data storage.

Technologies Used

- RabbitMQ for message queues
- MongoDB for data persistence
- CoinGecko API to retrieve cryptocurrency prices
- Newtonsoft.Json for serialization/deserialization

How It Works
- DataFetcher: A worker that periodically queries the CoinGecko API to fetch cryptocurrency price data and publishes it to RabbitMQ.
- DataProcessor: A worker that consumes the messages and inserts or updates the cryptocurrency data in MongoDB.
