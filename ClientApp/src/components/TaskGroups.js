import React, { useState, useEffect } from 'react';
import axios from 'axios';

const TaskGroups = () => {
    const [taskGroups, setTaskGroups] = useState([]);

    useEffect(() => {
        const getAll = async () => {
            const result = await axios(
                '/api/taskgroup',
            );

            setTaskGroups(result.data);
            console.log(result.data)
        };

        getAll();
    }, []);
    
    return (
        <ul>
            { taskGroups.map((group, index) => {
                return <li key={index}>{ group.name }</li>
            })}
        </ul>
    );
}

export default TaskGroups;