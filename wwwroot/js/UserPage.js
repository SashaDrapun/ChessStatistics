const buttonsCreateField = document.querySelectorAll('.createField');

for (let i = 0; i < buttonsCreateField.length; i++) {
    buttonsCreateField[i].addEventListener('click', onCreateFieldClick);
}


function deleteField(event) {
    event.preventDefault();
    const target = event.target;
    const deletedItem = target.parentNode.parentNode.parentNode;
    deletedItem.remove();
}

function onCreateFieldClick(e) {
    e.preventDefault();
    let countFields = 0;

    for (let i = 0; i < e.target.parentNode.childNodes.length; i++) {
        const childNode = e.target.parentNode.childNodes[i];
        if (childNode.classList !== undefined && childNode.classList.contains('fieldContainer')) {
            countFields++;
        }
    }

    if (countFields !== 5) {
        createField(e.target);
    }
}

function createField(node,nameField, typeField,fullEdit) {
        const field = document.createElement('div');
        field.className = 'fieldContainer';
            field.innerHTML = `<div class="row">
                                    <div class="col-4">
                                        <label>${document.querySelector('#FieldName').textContent}</label>
                                    </div>
                                    <div class="col-4">
                                        <label>${document.querySelector('#FieldType').textContent}</label>
                                    </div>
                                    <div class="col-4">
                                        <button class="btn btn-danger delete-field 
                                        ${fullEdit == false ? 'd-none' : ''}"
                                        onclick="deleteField(event)">${document.querySelector('#DeleteField').textContent}
                                        </button>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-4">
                                        <input type="text" name="nameField" class="form-control"
                                               value="${nameField !== undefined ? nameField : ''}" 
                                                placeholder="${document.querySelector('#FieldName').textContent}">
                                    </div>
                                    <div class="col-4">
                                        <select name="typeField">
                                            <option ${typeField === 'Числовой' ? 'selected' : ''}>${document.querySelector('#Numerical').textContent}</option>
                                            <option ${typeField === 'Строковый' ? 'selected' : ''}>${document.querySelector('#String').textContent}</option>
                                            <option ${typeField === 'Текстовый' ? 'selected' : ''}>${document.querySelector('#Text').textContent}</option>
                                            <option ${typeField === 'Дата' ? 'selected' : ''}>${document.querySelector('#Date').textContent}</option>
                                            <option ${typeField === 'Логический' ? 'selected' : ''}>${document.querySelector('#Logical').textContent}</option>
                                        </select>
                                    </div>
                                </div>`;
        node.before(field);
}

const buttonsDelete = document.querySelectorAll('.deleteCollection');

for (let i = 0; i < buttonsDelete.length; i++) {
    buttonsDelete[i].addEventListener('click', e => {
        document.querySelector('#idCollection').value = e.target.getAttribute("collectionId");
    });
}

const buttonsEdit = document.querySelectorAll('.editCollection');

for (let i = 0; i < buttonsEdit.length; i++) {
    buttonsEdit[i].addEventListener('click', e => {
        document.querySelector('#idCollectionEdit').value = e.target.getAttribute("collectionId");
        let fields = e.target.getAttribute("collectionFields").split(';');

        const buttonCreateFieldEdit = document.querySelector('#createFieldEdit');

            for (let i = 0; i < buttonCreateFieldEdit.parentNode.childNodes.length; i++) {
                const childNode = buttonCreateFieldEdit.parentNode.childNodes[i];
                if (childNode.classList !== undefined && childNode.classList.contains('fieldContainer')) {
                    childNode.remove();
                    i--;
                }
            }

        document.querySelector('#nameCollectionEdit').value = e.target.getAttribute("collectionName");
        document.querySelector('#descriptionEdit').value = e.target.getAttribute("collectionDescription");

        let countItemsInCollection = fields.length
        fullEdit = true;
        if (countItemsInCollection != 0) {
            fullEdit = false;
        }

        if (fields != "") {
            
            for (let i = 0; i < fields.length; i++) {
                let field = fields[i].split(',');
                createField(buttonCreateFieldEdit, field[0], field[1], fullEdit);
            }
        }  
    });
}
