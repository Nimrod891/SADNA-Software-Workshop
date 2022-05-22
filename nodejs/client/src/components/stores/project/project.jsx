import { useState } from 'react';
import classes from './project.module.css';
import { isExpired, isComplete } from '../grid/grid';
import Button from '@mui/material/Button';
import { BasicModal } from '../../modal/modal';
import { fetcher } from '../../../helpers/fetcher';
import { useAuth } from '../../../auth/auth-provider';

const InvestModal = ({ setOpen, setInvestAmount }) => {
    const [amount, setAmount] = useState(0);
    return (
        <div>
            <br></br>
            <input
                type='number'
                min='1'
                value={amount}
                onChange={(e) => setAmount(parseInt(e.target.value))}
            />
            <br></br>
            <br></br>
            <Button
                className={classes.button}
                sx={{ marginRight: '10px' }}
                variant='contained'
                onClick={(e) => {
                    setOpen(false);
                    setInvestAmount(amount);
                }}
            >
                Invest
            </Button>
            <Button
                className={classes.button}
                variant='contained'
                onClick={() => setOpen(false)}
            >
                Cancel
            </Button>
        </div>
    );
};

const Project = ({ project, setSelectedProject }) => {
    const [investModal, toggleInvestModal] = useState(false);
    const [investAmount, setInvestAmount] = useState(0);
    const auth = useAuth();

    const handleInvest = async (amount) => {
        setInvestAmount(amount);
        const result = await fetcher(`/projects/invest`, 'PUT', {
            amount,
            projectId: project._id,
        });
        const updatedProject = { ...project, raised: result.newRaised };
        setSelectedProject(updatedProject);
    };

    return (
        <div>
            <div className={classes.backButton}>
                <Button
                    sx={{ color: '#01579B' }}
                    onClick={() => setSelectedProject(null)}
                >
                    Go Back
                </Button>
            </div>
            <div className={classes.root}>
                    <div className={classes.card}>
                        <h1 className={classes.title}>{project.name}</h1>
                        {isExpired(project.expiration) ? (
                            <div className={classes.expired}>
                                Project Expired!{' '}
                                {new Date(
                                    project.expiration
                                ).toLocaleDateString('en-GB')}
                            </div>
                        ) : (
                            <div className={classes.alive}>
                                Open for donations until{' '}
                                {new Date(
                                    project.expiration
                                ).toLocaleDateString('en-GB')}
                            </div>
                        )}
                         <br/>
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
                        <br />
                        <div className={classes.description}>
                            {project.description}
                    </div>
                     <br/>
                         <div
                            className={classes.imgContainer}
                            onClick={() => setSelectedProject(project)}
                        >
                            <img
                                className={classes.center}
                                src={project.imgSrc[0]}
                                alt={project.name}
                            />
                    </div>
                     <br/>
                        <div className={classes.gallery}>
                            {project.imgSrc.slice(1).map((img) => {
                                return (
                                    <img
                                        className={classes.thumbnail}
                                        src={img}
                                        alt={project.name}
                                    />
                                );
                            })}
                            </div>
                        <div className={classes.center}>
                            {auth.user ? (
                                !isExpired(project.expiration) ? (
                                    <div
                                        className={classes.investButton}
                                        onClick={() => toggleInvestModal(true)}
                                    >
                                        <Button sx={{ color: '#01579B' }}>
                                            Invest
                                        </Button>
                                    </div>
                                ) : (
                                    <Button sx={{ color: '#01579B' }}>
                                        This project has expired!
                                    </Button>
                                )
                            ) : (
                                    <div className={classes.investButton}>
                                <Button sx={{ color: '#01579B' }} >
                                    Please login to invest in this project
                                        </Button>
                                 </div>
                            )}
                        </div>
                    </div>
            </div>
            {investModal && (
                <BasicModal
                    header='Enter investment amount'
                    content={
                        <InvestModal
                            setOpen={toggleInvestModal}
                            setInvestAmount={handleInvest}
                        />
                    }
                    setOpen={toggleInvestModal}
                />
            )}
        </div>
    );
};

export default Project;
