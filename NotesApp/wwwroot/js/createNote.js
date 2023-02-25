"use strict";

const uri = "/api/Note";

const noteColors = ["White", "Black", "Yellow", "Red", "Green", "Grey", "Blue"];

const colorDropdown = document.getElementById("note-colors-select");

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
  newElement.innerHTML = newTask.value;
  newElement.classList.add("list-group-item");
  todoList.insertBefore(newElement, todoList.children[0]);

  newTask.value = "";
});

form.onsubmit = async (e) => {
  e.preventDefault();

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
        content: `${todoItems[i].innerHTML}`,
        isDone: false,
        priorityOrder: i + 1,
      });
    }
    note.toDoList = todoArray;
  }

  console.log(note);

  fetch(uri, {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(note),
  })
    .then((response) => response.json())
    .catch((error) => console.error("Unable to add item.", error));
};
