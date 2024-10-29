
const URL = 'https://26.39.250.148:7097/api/Tareas';

export const getUserTask = (id) => {
    
    const options = {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    }

    return fetch(URL+"/"+id, options)
        .then(x => x.json())
        .catch(e => console.log(e));
}

export const getTask = () => {
    
    const options = {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    }

    return fetch(URL, options)
        .then(x => x.json())
        .catch(e => console.log(e));
}

export const addTask = (task) => {
    
    const options = {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(task)
    }

    return fetch(URL, options)
        .then(x => console.log(x))
        .catch(e => console.log(e));
}





