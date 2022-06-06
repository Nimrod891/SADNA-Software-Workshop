const mongoose = require('mongoose');

const storeSchema = new mongoose.Schema({
    name: {
        type: String,
        required: [true, 'Store name is missing'],
        unique: true,
    },
    founder: {
        type: String, // username
        required: [true, 'Store founder is missing'],
    },
});

const Stores = mongoose.model('stores', storeSchema);

module.exports = { Stores };
