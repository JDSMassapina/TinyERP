import { ValidationException } from "../../../common/models/exceptions/ValidationException";
import { FormValidationRules } from "../../../common/enum";
export class Model {
    public id: string;
    public name: string;
    public description: string;
    public validate(): boolean {
        let validation: ValidationException = new ValidationException();
        if (String.isNullOrWhiteSpace(this.name)) {
            validation.add("inventory.addOrUpdateCategory.validation.nameRequired");
        }
        if (!String.isNullOrWhiteSpace(this.name) && this.name.length > FormValidationRules.MaxNameLength) {
            validation.add("inventory.addOrUpdateCategory.validation.nameTooLong");
        }
        if (!String.isNullOrWhiteSpace(this.description) && this.description.length > FormValidationRules.MaxDescriptionLength) {
            validation.add("inventory.addOrUpdateCategory.validation.descriptionTooLong");
        }
        validation.throwIfHasError();
        return !validation.hasError();
    }
    public import(item: any) {
        this.id = item.id;
        this.name = item.name;
        this.description = item.description;
    }
}