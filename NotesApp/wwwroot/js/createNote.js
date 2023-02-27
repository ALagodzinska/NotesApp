"use strict";

const uri = "/api/Note";

const noteColors = ["White", "Black", "Yellow", "Red", "Green", "Grey", "Blue"];

const deleteIcon = `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash" viewBox="0 0 16 16">
                            <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z" />
                            <path fill-rule="evenodd" d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z" />
                        </svg>`;

// colors
const colorDropdown = document.getElementById("note-colors-select");

// note title
const noteTitle = document.getElementById("note-title-input");

// radio buttons
const noteTypeArea = document.querySelector(".note-type-area");
const textNoteRadio = document.getElementById("type-text-radio");
const todoNoteRadio = document.getElementById("type-todo-radio");
// input areas
const textNoteInputArea = document.getElementById("text-note-input");
const todoListInputArea = document.getElementById("todo-list-input"); // whole todo block

// add new todo elements
const addTodoBtn = document.getElementById("btn-add-task");
const newTask = document.getElementById("new-todo");

// todo list
const todoList = document.getElementById("todo-list");
// textarea
const textarea = document.getElementById("textarea-input");

//form element
const form = document.getElementById("create-form");

// adding colors to dropdown
noteColors.forEach((color) => {
  var option = document.createElement("option");
  option.value = color;
  option.innerHTML = color;
  colorDropdown.appendChild(option);
});

// close open text or list input on radio click
noteTypeArea.addEventListener("click", function (event) {
  if (event.target.classList.contains("form-check-input")) {
    if (event.target === textNoteRadio) {
      // clear todolist
      todoList.innerHTML = "";
      newTask.value = "";

      textNoteInputArea.classList.remove("hidden");
      todoListInputArea.classList.add("hidden");
    } else {
      // clear textarea
      textarea.value = "";

      todoListInputArea.classList.remove("hidden");
      textNoteInputArea.classList.add("hidden");
    }
  }
});

// add new task to the todo list
addTodoBtn.addEventListener("click", (event) => {
  event.preventDefault();

  if (!newTask.value.trim()) {
    newTask.classList.add("is-invalid");
    return;
  }

  newTask.classList.remove("is-invalid");

  const newElement = document.createElement("li");
  const deleteSpan = document.createElement("span");
  deleteSpan.classList.add("delete-icon");
  deleteSpan.innerHTML = deleteIcon;

  // delete element when pressing on span
  deleteSpan.addEventListener("click", function (e) {
    e.preventDefault();
    newElement.parentNode.removeChild(newElement);
  });

  newElement.innerHTML = newTask.value;
  newElement.appendChild(deleteSpan);
  newElement.classList.add("list-group-item");
  todoList.insertBefore(newElement, todoList.children[0]);

  newTask.value = "";
});

// method that converts data from form into an object
const createNoteObject = function () {
  let formNote = new FormData(form);

  const note = {};

  for (let [name, value] of formNote) {
    note[name] = value;
  }

  if (note["color"]) {
    let colorIndex = noteColors.indexOf(note["color"]);
    if (colorIndex === -1) {
      note["color"] = null;
    } else {
      note["color"] = colorIndex;
    }
  }

  if (note["type"] === "TextNote") {
    note["type"] = 0;
  } else if (note["type"] === "ToDoList") {
    note["type"] = 1;

    const todoItems = todoList.getElementsByTagName("li");

    let todoArray = [];

    for (var i = 0; i < todoItems.length; ++i) {
      todoArray.push({
        content: `${todoItems[i].innerText}`,
        isDone: false,
        priorityOrder: i + 1,
      });
    }
    note.toDoList = todoArray;
  }

  return note;
};

// validate data
const validate = function (note) {
  // check if form is valid
  let hasErrors = false;

  // check title
  const title = note.title.trim();
  if (!title) {
    noteTitle.classList.add("is-invalid");
    hasErrors = true;
  } else {
    noteTitle.classList.remove("is-invalid");
  }
  //check color
  if (note.color === undefined) {
    colorDropdown.classList.add("is-invalid");
    hasErrors = true;
  } else {
    colorDropdown.classList.remove("is-invalid");
  }
  // check type
  if (note.type === undefined) {
    textNoteRadio.classList.add("is-invalid");
    todoNoteRadio.classList.add("is-invalid");
    hasErrors = true;
  } else {
    textNoteRadio.classList.remove("is-invalid");
    todoNoteRadio.classList.remove("is-invalid");
  }

  // check if textype and textarea is filled
  if (note.type === 0 && !note.textContent) {
    textarea.classList.add("is-invalid");
    hasErrors = true;
  } else {
    textarea.classList.remove("is-invalid");
  }

  // check if todotype and list have at least one item
  if (note.type === 1 && note.toDoList.length === 0) {
    newTask.classList.add("is-invalid");
    hasErrors = true;
  } else {
    newTask.classList.remove("is-invalid");
  }

  return hasErrors;
};

// submits create form
form.onsubmit = async (e) => {
  e.preventDefault();

  // creates an object from form data
  const note = createNoteObject();

  // validate
  if (validate(note)) {
    // do nothing when we have errors
    return;
  }

  // send data to an api
  // await because its async
  await fetch(uri, {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(note),
  })
    .then((response) => response.json())
    .catch((error) => console.error("Unable to add item.", error));

  // redirects to an index page
  window.location.replace("/");
};
