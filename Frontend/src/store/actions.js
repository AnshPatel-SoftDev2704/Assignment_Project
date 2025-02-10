import {LOGIN} from './actionsTypes'

export const login = (user) => ({
    type : LOGIN,
    payload: {user}
})