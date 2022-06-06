const mongoose = require('mongoose');

const userSchema = new mongoose.Schema({
    user: {
        type: String,
        required: [true, 'User name is missing'],
        unique: true,
    },
    password: {
        type: String,
        required: [true, 'Password is missing'],
    },
    type: {
        type: String,
        default: 'member', // admin | member
    },
});

const Users = mongoose.model('users', userSchema);

module.exports = { Users };
