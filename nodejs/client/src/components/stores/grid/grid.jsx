import { GridItem } from './grid-item/grid-item';
import classes from './grid.module.css';

export const isExpired = (date) => {
    const now = new Date();
    const expiration = new Date(date);
    return expiration < now;
};

export const isComplete = (raised, amount) => {
    return raised >= amount;
};

const Grid = ({ projects, setSelectedProject }) => {
    const liveProjects = projects.filter(
        (project) => !isExpired(project.expiration)
    );

    const expiredCompleted = projects.filter(
        (project) =>
            isExpired(project.expiration) &&
            isComplete(project.raised, project.amount)
    );

    const expiredIncomplete = projects.filter(
        (project) =>
            isExpired(project.expiration) &&
            !isComplete(project.raised, project.amount)
    );

    return (
        <div className={classes.root}>
            <div className={classes.grid}>
                {liveProjects.map((project, index) => {
                    return (
                        <GridItem
                            key={index}
                            project={project}
                            setSelectedProject={setSelectedProject}
                        />
                    );
                })}
            </div>
            <br />
            <hr></hr>
            <div className={classes.grid}>
                {expiredIncomplete.map((project, index) => {
                    return (
                        <GridItem
                            key={index}
                            project={project}
                            setSelectedProject={setSelectedProject}
                        />
                    );
                })}
            </div>
            <br />
            <hr></hr>
            <div className={classes.grid}>
                {expiredCompleted.map((project, index) => {
                    return (
                        <GridItem
                            key={index}
                            project={project}
                            setSelectedProject={setSelectedProject}
                        />
                    );
                })}
            </div>
        </div>
    );
};

export default Grid;
