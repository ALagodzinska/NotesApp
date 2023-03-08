const uri = "api/Note";
const uriToGetUser = "api/User";

let allNotes;

// container for all notes
const allNotesConatiner = document.getElementById("notes-index");

const paginationLimit = 9;

// Pagination buttons
const prevPageBtn = document.getElementById("prev-button");
const nextPageBtn = document.getElementById("next-button");
let curPageIndex = 1;
let pageCount = 1;

let currentUser = {};

// const getCurrentUser = () =>
//   fetch(uriToGetUser)
//     .then((response) => response.json())
//     .catch((error) => console.error("Unable to get current user.", error));

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

// const getNotes = function () {
//   return fetch(uri)
//     .then((response) => response.json())
//     .then((data) => {
//       // get only user notes and shared notes
//       const userNotes = data.filter(
//         (note) =>
//           note.username === currentUser.name ||
//           note.sharedWithUsers.find((u) => u.userName === currentUser.name)
//       );
//       displayNotes(userNotes);
//     })
//     .catch((error) => console.error("Unable to get items.", error));
// };

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

  displayNotes(allNotes.slice(0, paginationLimit));
  addPagination();
};

const notesDisplayOnPage = function (index) {
  const startIndex = paginationLimit * (index - 1);
  const endIndex = paginationLimit * index;

  curPageIndex = index;

  disableArrows();
  displayNotes(allNotes.slice(startIndex, endIndex));
};

const disableArrows = function () {
  prevPageBtn.disabled = curPageIndex === 1 ? true : false;
  nextPageBtn.disabled = curPageIndex === pageCount ? true : false;
};

const addPagination = function () {
  // Pagination
  const paginationNumbers = document.getElementById("pagination-numbers");

  pageCount = Math.ceil(allNotes.length / paginationLimit);
  disableArrows();

  const appendPageNumber = (index) => {
    const pageNumber = document.createElement("button");
    pageNumber.className = "pagination-number";
    pageNumber.innerHTML = index;
    pageNumber.setAttribute("page-index", index);
    pageNumber.setAttribute("aria-label", "Page " + index);

    pageNumber.addEventListener("click", function () {
      notesDisplayOnPage(index);
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
    notesDisplayOnPage(curPageIndex - 1);
  }
});

nextPageBtn.addEventListener("click", function () {
  if (curPageIndex !== pageCount) {
    notesDisplayOnPage(curPageIndex + 1);
  }
});

// getCurrentUser().then((user) => {
//   // set user before loading notes
//   currentUser = user;
//   // load notes
//   getNotes();
// });

let startProgram = async function () {
  // get user
  currentUser = await getCurrentUser();
  // render notes
  await getNotes();
};

startProgram();
