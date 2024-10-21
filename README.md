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

How to Run
- Ensure you have [Docker](https://www.docker.com/get-started) installed on your machine.
- Run:
```bash
   git clone https://github.com/ricardocossta/crypto-coin-tracker.git
   cd crypto-coin-tracker
   cd CryptoCoinTracker
   docker-compose up --build
```
- Once the services are up and running, you can access the RabbitMQ management interface at http://localhost:15672 (default username: guest, password: guest) and MongoDB on localhost:27017.
- The DataFetcher and DataProcessor services will automatically start and communicate with each other using RabbitMQ and store data in MongoDB.
