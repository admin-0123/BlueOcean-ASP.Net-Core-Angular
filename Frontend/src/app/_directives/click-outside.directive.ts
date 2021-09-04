import {
    Directive,
    ElementRef,
    EventEmitter,
    HostListener,
    Output
} from '@angular/core';

@Directive({
    selector: '[appClickOutside]'
})
export class ClickOutsideDirective {
    @Output() clickOutside: EventEmitter<any> = new EventEmitter();

    constructor(
      private _elementRef: ElementRef
    ) { }

    @HostListener('document:click', ['$event.target']) onMouseEnter(targetElement: any) {
        if (!this._elementRef.nativeElement.contains(targetElement)) {
            this.clickOutside.emit(null);
        }
    }
}
