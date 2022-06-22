import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCirclePlus, faCircleMinus, faPenToSquare, faSort } from '@fortawesome/free-solid-svg-icons';
import axios from 'axios';
import styles from './taskgroups.module.css';

const TaskGroups = () => {
    const [taskGroups, setTaskGroups] = useState([]);
    const [showSortModal, setShowSortModal] = useState(false);
    const history = useHistory();

    const getAll = async (orderBy = "id") => {
        const config = {
            method: 'get',
            url: `/api/taskgroup/?orderBy=${orderBy}`
        }
        
        const result = await axios(config);
        setTaskGroups(result.data);
        console.log(result.data)
    };
    
    useEffect(() => {
        getAll();
    }, []);
    
    const removeTaskGroup = async (id) => {
        const config = {
            method: 'delete',
            url: `/api/taskgroup/${id}`
        }
        
        await axios(config)
            .then(() =>  getAll());
    }
    
    const redirectToTaskGroup = (id) => {
        history.push(`/task/${id}`);
    }
    
    return (
        <table className="table table-striped table-dark">
            <thead>
            <tr>
                <th scope="col" className="position-relative">
                    <FontAwesomeIcon icon={ faSort } size="lg" onClick={ () => setShowSortModal(!showSortModal) } />
                    { showSortModal &&
                        <div className={ styles.sortModal }>
                            <div onClick={ () => getAll("name") }>Task name</div>
                            <div onClick={ () => getAll("tasks") }>Number of tasks</div>
                        </div>
                    }
                </th>
                <th scope="col">Name</th>
                <th scope="col">User Tasks</th>
                <th scope="col"></th>
            </tr>
            </thead>
            <tbody>
                { taskGroups.map((group, index) => {
                    return (
                        <tr key={ index }>
                            <th scope="row">{ index }</th>
                            <td>{ group.name }</td>
                            <td>{ group.userTasks.length }</td>
                            <td className={ styles.taskGroupsIcons }>
                                <FontAwesomeIcon onClick={ () => redirectToTaskGroup(group.id) } icon={ faCirclePlus } size="xl"/>
                                <FontAwesomeIcon onClick={ () => removeTaskGroup(group.id) } icon={ faCircleMinus } size="xl"/>
                                <FontAwesomeIcon icon={ faPenToSquare } size="xl"/>
                            </td>
                        </tr>
                    )
                })}
            </tbody>
        </table>
    );
}

export default TaskGroups;