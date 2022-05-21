const jwt = require('jsonwebtoken');
const config = require('config');
const jwtConfig = config.get('jwt');

const requireAuth = (req, res, next) => {
    const token = req.cookies.jwt;

    if (!token) {
        res.status(401).send('Please login');
        return;
    }

    jwt.verify(token, jwtConfig.secret, (err, decodedToken) => {
        if (err) {
            console.log(err.message);
            res.send('Token invalid');
            return;
        }

        res.locals.token = decodedToken;
        next();
    });
};

module.exports = { requireAuth };
