import React, { useState, useEffect } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCirclePlus, faCircleMinus, faPenToSquare, faSort } from '@fortawesome/free-solid-svg-icons';
import axios from 'axios';
import styles from './taskgroup.module.css';

const TaskGroups = () => {
    const [taskGroups, setTaskGroups] = useState([]);

    const getAll = async () => {
        const config = {
            method: 'get',
            url: '/api/taskgroup'
        }
        
        const result = await axios(config);
        setTaskGroups(result.data);
    };
    
    useEffect(() => {
        getAll();
    }, []);
    
    const removeTaskGroup = async (id) => {
        const config = {
            method: 'delete',
            url: `/api/taskgroup/${id}`
        }
        
        const result = await axios(config)
            .then(() =>  getAll());
    }
    
    return (
        <table className="table table-striped table-dark">
            <thead>
            <tr>
                <th scope="col"></th>
                <th scope="col">Name</th>
                <th scope="col">User Tasks</th>
                <th scope="col"></th>
            </tr>
            </thead>
            <tbody key={ taskGroups }>
                { taskGroups.map((group, index) => {
                    return (
                        <tr key={ index }>
                            <th scope="row">{ index }</th>
                            <td>{ group.name }</td>
                            <td>{ group.userTasks.length }</td>
                            <td className="col-4 col-md-3 col-lg-2">
                                <FontAwesomeIcon icon={ faCirclePlus } size="lg"/>
                                <FontAwesomeIcon onClick={ () => removeTaskGroup(group.id) } icon={ faCircleMinus } size="lg"/>
                                <FontAwesomeIcon icon={ faPenToSquare } size="lg"/>
                                <FontAwesomeIcon icon={ faSort } size="lg" />
                            </td>
                        </tr>
                    )
                })}
            </tbody>
        </table>
    );
}

export default TaskGroups;