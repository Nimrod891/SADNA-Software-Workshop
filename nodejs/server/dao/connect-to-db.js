const mongoose = require('mongoose');
const config = require('config');
const mongo = config.get('mongo');

const connectToDb = async () => {
    try {
        await mongoose.connect(mongo.url, {
            useNewUrlParser: true,
            useUnifiedTopology: true,
        });
        console.log('Connected to MongoDB');
    } catch (err) {
        console.log(err);
    }
};

module.exports = { connectToDb };
