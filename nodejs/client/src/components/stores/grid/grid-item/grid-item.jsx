import classes from './grid-item.module.css';
import { isExpired, isComplete } from '../grid';

export const GridItem = ({ project, setSelectedProject }) => {
    return (
        <div className={classes.board}>
            <div className={classes.card}>
                <div
                    className={classes.imgContainer}
                    onClick={() => {
                        if (isExpired(project.expiration)) {
                            alert('Project Expired!');
                        } else {
                            setSelectedProject(project);
                        }
                    }}
                >
                    <img
                        className={classes.img}
                        src={project.imgSrc[0]}
                        alt={project.name}
                    />
                </div>
                <h1 className={classes.title}>{project.name}</h1>
                {isExpired(project.expiration) ? (
                    <div className={classes.expired}>
                        Project Expired!{' '}
                        {new Date(project.expiration).toLocaleDateString(
                            'en-GB'
                        )}
                    </div>
                ) : (
                    <div className={classes.alive}>
                        Open for donations until{' '}
                        {new Date(project.expiration).toLocaleDateString(
                            'en-GB'
                        )}
                    </div>
                )}

                {isComplete(project.raised, project.amount) ? (
                    <div className={classes.tagComplete}>
                        <span>{project.raised}</span> /{' '}
                        <span>{project.amount}</span>
                    </div>
                ) : (
                    <div className={classes.tag}>
                        <span>{project.raised}</span> /{' '}
                        <span>{project.amount}</span>
                    </div>
                )}
            </div>
        </div>
    );
};
