import {LOGIN } from './actionsTypes';

const loginReducer = (state = [], action) => {
  switch (action.type) {
    case LOGIN:
      return [...state, action.payload.user];
    default:
      return state; 
  }
};

export default loginReducer;
