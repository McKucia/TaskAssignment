import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { useHistory } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {faCheck, faCircleMinus, faArrowLeftLong } from '@fortawesome/free-solid-svg-icons';
import axios from 'axios';
import styles from './taskgroup.module.css';

const Group = () => {
    const params = useParams();
    const [taskGroup, setTaskGroup] = useState({});
    const [name, setName] = useState("");
    const [deadline, setDeadline] = useState();
    const [groupName, setGroupName] = useState("");
    const history = useHistory();

    const getTaskGroup = async () => {
        console.log(params.id)
        const config = {
            method: 'get',
            url: `/api/taskgroup/${params.id}`
        }

        const result = await axios(config);
        setTaskGroup(result.data);
    };

    useEffect(() => {
        getTaskGroup();
    }, []);

    const addTask = async (event) => {
        event.preventDefault();

        const config = {
            method: 'post',
            url: `/api/taskgroup/${params.id}`,
            data: {
                name: name,
                deadline: deadline
            }
        }

        const result = await axios(config)
            .then(() =>  getTaskGroup());
    }

    const removeTask = async (id) => {
        const config = {
            method: 'delete',
            url: `/api/taskgroup/task-${id}`
        }

        await axios(config)
            .then(() => getTaskGroup());
    }
    
    const changeName = async () => {
        console.log(groupName)
        const config = {
            method: 'post',
            url: `/api/taskgroup/${params.id}/${groupName}`
        }

        await axios(config);
        setGroupName(groupName);
    }

    const redirectToTaskGroups = (id) => {
        history.push(`/task-groups`);
    }

    return (
        <div id="container" className="mt-4 d-flex flex-column">
            <div className={ styles.groupNameBox }>
                <FontAwesomeIcon icon={ faArrowLeftLong } size="xl" className="mr-4" onClick={() => redirectToTaskGroups() } />
                <input onChange={ (e) => setGroupName(e.target.value) } 
                       className={ styles.groupName } 
                       placeholder={ taskGroup.name } />
                <FontAwesomeIcon icon={ faCheck } size="xl" className="ml-4" onClick={() => changeName() }/>
            </div>
            <div className="container row mt-3">
                <div className="col-7 m-0 p-0">
                    <table className="table table-striped table-dark">
                        <thead>
                        <tr>
                            <th scope="col"></th>
                            <th scope="col">Name</th>
                            <th scope="col">Deadline</th>
                            <th scope="col">Status</th>
                            <th scope="col"></th>
                        </tr>
                        </thead>
                        <tbody>
                            { taskGroup.userTasks ? taskGroup.userTasks.map((task, index) => {
                                return (
                                    <tr key={ index }>
                                        <th scope="row">{ index }</th>
                                        <td>{ task.name }</td>
                                        <td>{ task.deadline }</td>
                                        <td>{ task.status }</td>
                                        <td class={ styles.GroupIcons }>
                                            <FontAwesomeIcon onClick={ () => removeTask(task.id) } 
                                                             icon={ faCircleMinus } 
                                                             size="xl"/>
                                        </td>
                                    </tr>
                                )
                            }) : <div className={ styles.loader }></div> }
                        </tbody>
                    </table>
                </div>
                <div className="col-5 m-0 px-2">
                    <div id={ styles.addUser }>
                        <h3>Add new task</h3>
                        <form onSubmit={ addTask }>
                            <div className="form-group">
                                <label htmlFor="name">Name</label>
                                <input type="text" className="form-control" 
                                       id="name" placeholder="Enter task name"
                                       value={ name } onChange={ (e) => setName(e.target.value) }/>
                            </div>
                            <div className="form-group">
                                <label htmlFor="deadline">Deadline</label>
                                <input type="date" className="form-control" id="deadline"
                                       value={ deadline } onChange={ (e) => setDeadline(e.target.value) }/>
                            </div>
                            <button type="submit" className="btn btn-success">Add</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Group;