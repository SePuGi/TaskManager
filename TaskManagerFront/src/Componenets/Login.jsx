import { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getUser } from "../Controllers/UserCont";
import ContextLogin from "../Controllers/ContextLogin";

function Login() {
    const navigate = useNavigate();
    const [email, setEmail] = useState("");
    const [pswd, setPswd] = useState("");
    const { canviaLog, canviaUser, logged } = useContext(ContextLogin)

    function enviarForm(e) {
        e.preventDefault();
        getUser({ "correo": email, "contraseña": pswd })
            .then(x => {
                canviaLog(x.value)
                canviaUser({ id: x.user.id, nombre: x.user.nombre })
            })
    }

    useEffect(() => {
        if (logged) {
            navigate("/home");
        }
    }, [logged])

    return (
        <>
            <form onSubmit={enviarForm}>
                <label htmlFor="nom">Nom:</label>
                <input type="email" id="nom" value={email} onChange={(e) => setEmail(e.target.value)} required />

                <label htmlFor="password">Password</label>
                <input type="password" id="password" value={pswd} onChange={(e) => setPswd(e.target.value)} required />

                <button type="submit"  > Send</button>
            </form>
        </>
    )
}

export default Login;