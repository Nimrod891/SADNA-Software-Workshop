const { Users } = require('../models/users-model');

class UsersDao {
    async findOneByUsername(username) {
        const user = await Users.findOne({ user: username });
        return user;
    }

    async findByLoginData(username, password) {
        const user = await Users.findOne({ user: username, password });
        return user;
    }

    async create(user) {
        const newUser = await Users.create(user);
        return newUser;
    }
}

module.exports = { UsersDao };
