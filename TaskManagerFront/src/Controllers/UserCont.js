
const URL = 'https://26.39.250.148:7097/api/Usuarios/login';

export const getUser = (user) => {
    
    const options = {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(user)
    }

    return fetch(URL, options)
        .then(x => x.json())
        .catch(e => console.log(e));
}