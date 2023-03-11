const uri = "api/Note";
const uriToGetUser = "api/User";

let currentUser = {};
let allNotes;
let sortedNotes;

// Filter parameters
let displayType = "all"; // all | text | todo
let displayCreator = "all"; // all | my | shared
let filterBy = null;

// container for all notes
const allNotesConatiner = document.getElementById("notes-index");

// Pagination buttons
const paginationLimit = 9;
const prevPageBtn = document.getElementById("prev-button");
const nextPageBtn = document.getElementById("next-button");
let curPageIndex = 1;
let pageCount = 1;
const paginationNumbers = document.getElementById("pagination-numbers");

// Filters buttons
const dropdownBtn = document.querySelector(".dropdown-toggle");
const sortTextNoteBtn = document.getElementById("text-note-sort");
const sortToDoBtn = document.getElementById("todo-sort");
const sortMyNotesBtn = document.getElementById("my-sort");
const sortSharedBtn = document.getElementById("shared-sort");
const displayAllBtn = document.getElementById("notes-all");
// Search btn
const searchInput = document.getElementById("search-input");
const searchBtn = document.getElementById("search-btn");

const getCurrentUser = async () => {
  let response = await fetch(uriToGetUser);
  let user = await response.json();
  return user;
};

// add read more btn to the long text
const addReadMoreBtn = function (currentNote) {
  // note p elemnet
  const noteContentElement = currentNote.querySelector(".card-text");

  // only if text is longer than four lines
  if (
    noteContentElement.offsetHeight < noteContentElement.scrollHeight ||
    noteContentElement.offsetWidth < noteContentElement.scrollWidth
  ) {
    const buttonReadMore = document.createElement("button");
    buttonReadMore.classList.add("btn-readmore");
    buttonReadMore.innerHTML = "Read More";
    // add event on click
    buttonReadMore.addEventListener("click", function () {
      if (noteContentElement.classList.contains("cutoff-text")) {
        noteContentElement.classList.remove("cutoff-text");
        buttonReadMore.innerHTML = "Read Less";
      } else {
        noteContentElement.classList.add("cutoff-text");
        buttonReadMore.innerHTML = "Read More";
      }
    });

    noteContentElement.after(buttonReadMore);
  }
};

const formatDate = function (date) {
  const jsDate = new Date(Date.parse(date));

  // adds 0 if only one number
  const day = `${jsDate.getDate()}`.padStart(2, 0);
  const month = `${jsDate.getMonth() + 1}`.padStart(2, 0);
  const year = jsDate.getFullYear();

  return `${day}/${month}/${year}`;
};

const createNoteHTML = function (note) {
  // display todo or text based on note type
  const noteContent =
    note.type === 1
      ? `${note.toDoList
          .map((todo) => {
            const isDone = todo.isDone ? "checked" : "";
            const checkbox = `<input type="checkbox" ${isDone} disabled>`;
            return `${checkbox} ${todo.content}<br/>`;
          })
          .join("")}`
      : `${note.textContent}`;

  // display buttons based on your note or shared
  const buttons =
    note.username === currentUser.name
      ? `<a href="/Notes/Edit?id=${note.id}" class="btn btn-sm btn-outline-secondary">Edit</a>
                                <a href="/Notes/Delete?id=${note.id}" class="btn btn-sm btn-outline-secondary">Delete</a>
                                <a href="/Notes/Share?id=${note.id}" class="btn btn-sm btn-outline-secondary share-btn"><span>Share</span></a>`
      : `<a href="/Notes/Remove?id=${note.id}" class="btn btn-sm btn-outline-secondary">Remove</a>`;

  // display date of creation or user who shared his note
  const smallContent =
    note.username === currentUser.name
      ? formatDate(note.creationDate)
      : `Created by ${note.username}`;

  const noteHtml = `<div class="col-md-4 one-note" id="note-${note.id}">
                    <div class="card mb-4 box-shadow ${note.colorClass} one-note-di">
                        <div class="card-body">
                            <h5 class="card-title">${note.title}</h5>
                            <p class="card-text cutoff-text">
                                ${noteContent}
                            </p>
                            <div class="btn-group btn-light btn-outline-light">
                                ${buttons}
                            </div> 
                            <small>${smallContent}</small>
                        </div>                        
                    </div>
                </div>`;

  return noteHtml;
};

const displayNotes = function (data) {
  // clear the notes container
  allNotesConatiner.innerHTML = "";

  // create an html element foreach of them
  data.forEach((note) => {
    const noteHtml = createNoteHTML(note);

    // add note to page
    allNotesConatiner.insertAdjacentHTML("beforeend", noteHtml);

    // get current note element
    const currentNote = document.getElementById(`note-${note.id}`);

    // add read more btn
    addReadMoreBtn(currentNote);
  });
};

const renderNotes = function () {
  // create a copy
  sortedNotes = [...allNotes];

  if (displayType === "text") {
    sortedNotes = sortedNotes.filter((item) => item.type === 0);
  }

  if (displayType === "todo") {
    sortedNotes = sortedNotes.filter((item) => item.type === 1);
  }

  if (displayCreator === "my") {
    sortedNotes = sortedNotes.filter(
      (item) => item.username === currentUser.name
    );
  }

  if (displayCreator === "shared") {
    sortedNotes = sortedNotes.filter(
      (item) => item.username !== currentUser.name
    );
  }

  if (filterBy) {
    sortedNotes = sortedNotes.filter((item) =>
      item.title.toLowerCase().includes(filterBy)
    );
  }

  displayNotes(sortedNotes.slice(0, paginationLimit));
  // render pagination
  addPagination(sortedNotes);
};

const getNotes = async function () {
  let response = await fetch(uri);
  let data = await response.json();

  // get only user notes and shared notes
  const userNotes = data.filter(
    (note) =>
      note.username === currentUser.name ||
      note.sharedWithUsers.find((u) => u.userName === currentUser.name)
  );

  allNotes = userNotes;
  renderNotes();
};

const notesDisplayOnPage = function (index) {
  const startIndex = paginationLimit * (index - 1);
  const endIndex = paginationLimit * index;

  curPageIndex = index;

  disableArrows();
  displayNotes(sortedNotes.slice(startIndex, endIndex));
};

const disableArrows = function () {
  prevPageBtn.disabled = curPageIndex === 1 ? true : false;
  nextPageBtn.disabled = curPageIndex === pageCount ? true : false;
};

const changePage = function (pageNumber) {
  // display notes
  notesDisplayOnPage(pageNumber);
  // clear number active classes
  [...paginationNumbers.children].forEach((b) => b.classList.remove("active"));
  // activate page number
  const pageBtn = document.querySelector(`[page-index="${pageNumber}"]`);
  pageBtn.classList.add("active");
};

const addPagination = function (notes) {
  // Pagination
  paginationNumbers.innerHTML = "";

  pageCount = Math.ceil(notes.length / paginationLimit);
  disableArrows();

  const appendPageNumber = (index) => {
    const pageNumber = document.createElement("button");
    pageNumber.className = "pagination-number";
    pageNumber.innerHTML = index;
    pageNumber.setAttribute("page-index", index);
    pageNumber.setAttribute("aria-label", "Page " + index);

    pageNumber.addEventListener("click", function () {
      changePage(index);
    });

    paginationNumbers.appendChild(pageNumber);
  };

  const getPaginationNumbers = () => {
    for (let i = 1; i <= pageCount; i++) {
      appendPageNumber(i);
    }
  };

  getPaginationNumbers();
};

prevPageBtn.addEventListener("click", function () {
  if (curPageIndex !== 1) {
    changePage(curPageIndex - 1);
  }
});

nextPageBtn.addEventListener("click", function () {
  if (curPageIndex !== pageCount) {
    changePage(curPageIndex + 1);
  }
});

// Filters
sortTextNoteBtn.addEventListener("click", function () {
  displayCreator = "all";
  dropdownBtn.innerHTML = "";
  dropdownBtn.innerHTML = "Options: Text Notes";
  displayType = "text";
  renderNotes();
});

sortToDoBtn.addEventListener("click", function () {
  displayCreator = "all";
  dropdownBtn.innerHTML = "";
  dropdownBtn.innerHTML = "Options: ToDo Lists";
  displayType = "todo";
  renderNotes();
});

sortMyNotesBtn.addEventListener("click", function () {
  displayType = "all";
  dropdownBtn.innerHTML = "";
  dropdownBtn.innerHTML = "Options: My Notes";
  displayCreator = "my";
  renderNotes();
});

sortSharedBtn.addEventListener("click", function () {
  displayType = "all";
  dropdownBtn.innerHTML = "";
  dropdownBtn.innerHTML = "Options: Shared Notes";
  displayCreator = "shared";
  renderNotes();
});

displayAllBtn.addEventListener("click", function () {
  dropdownBtn.innerHTML = "";
  dropdownBtn.innerHTML = "Options: All Notes";
  displayType = "all";
  displayCreator = "all";
  renderNotes();
});

searchBtn.addEventListener("click", function () {
  filterBy = searchInput.value;
  renderNotes();
});

let startProgram = async function () {
  // get user
  currentUser = await getCurrentUser();
  // render notes
  await getNotes();
};

startProgram();
