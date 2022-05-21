import { Stores } from '../stores/stores';
import classes from './home.module.css';

const Home = () => {
    return (
        <div className={classes.root}>
            <div className={classes.background}></div>
            <Stores />
        </div>
    );
};

export default Home;
