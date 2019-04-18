export class FuncHelper {
    static callFuncCheckNotNull(func: () => void = null) {
        if (func != null) {
            func();
        }
    }

    static isNull(value: any): boolean {
        return (value == null || value == '');
    }

    static ConvertToParamString(obj: any): string {
        let props = Object.getOwnPropertyNames(obj);
        let rs = '';
        for (var i = 0; i < props.length; i++) {
            rs=`${props[i]}=${encodeURIComponent(obj[props[i]])}&`
        }

        return rs;
    }

    static isFunction(f): f is Function {
        return f instanceof Function;
    }

    static syncData(from: any, to: any): any {
        let rs = to;
        if (FuncHelper.isNull(rs)) rs = {};
        let props = Object.getOwnPropertyNames(from);
        for (var i = 0; i < props.length; i++) {
            rs[props[i]] = from[props[i]];
        }
        return rs;
    }

    static getTime(date?: any) {
        try {
            let dtemp = new Date(date);
            return dtemp != null ? dtemp.getTime() : 0;
        } catch (e) {
            return 0;
        }
    }

    static clone(item: any): any {
        if (!item) { return item; } // null, undefined values check

        var types = [Number, String, Boolean],
            result: any;

        // normalizing primitives if someone did new String('aaa'), or new Number('444');
        types.forEach(function (type) {
            if (item instanceof type) {
                result = type(item);
            }
        });

        if (typeof result == "undefined") {
            if (Object.prototype.toString.call(item) === "[object Array]") {
                result = [];
                item.forEach((child: any, index: number, array: any) => {
                    result[index] = FuncHelper.clone(child);
                });
            } else if (typeof item == "object") {
                // testing that this is DOM
                if (item.nodeType && typeof item.cloneNode == "function") {
                    result = item.cloneNode(true);
                } else if (!item.prototype) { // check that this is a literal
                    if (item instanceof Date) {
                        result = new Date(item);
                    } else {
                        // it is an object literal
                        result = {};
                        for (var i in item) {
                            result[i] = FuncHelper.clone(item[i]);
                        }
                    }
                } else {
                    result = item;
                }
            } else {
                result = item;
            }
        }

        return result;
    }
    static removeItemInArray<T>(array: T[], key: string, value: any): T[] {
        let result: T[] = []
        for (var i = 0; i < array.length; i++) {
            if (array[i][key] !== value) {
                result.push(array[i]);
            }
        }
        return result;
    }

    static getCurrenyFormat(num: number): string {
        return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1.')
    }
}
