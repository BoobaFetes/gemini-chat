import { IDocumentDto, IProjectDto } from '@aogenai/domain';
import { Button, TextField } from '@mui/material';
import { FC, memo, useCallback } from 'react';
import {
  CustomAccordion,
  FormWithButtons,
  IFormWithButtonsProps,
} from '../common';
import { DocumentCollection } from '../Document';
import { onPropertyChange } from '../tools';

export interface IProjectItemProps extends IFormWithButtonsProps<IProjectDto> {
  onDocumentDonwloaded?: (document: IDocumentDto) => void;
  onUserStoryEditorCLick?: (item: IProjectDto) => void;
}

export const ProjectItem: FC<IProjectItemProps> = memo(
  ({
    data,
    save,
    reset,
    remove,
    children,
    onDocumentDonwloaded,
    onUserStoryEditorCLick,
    ...htmlAttributes
  }) => {
    const renderChildren = useCallback<
      NonNullable<IProjectItemProps['children']>
    >(
      (item, setItem) => {
        const IsEdition = Boolean(item?.id);
        return (
          <>
            <TextField
              label="Name"
              name="Name"
              value={item.name}
              onChange={onPropertyChange({ item, setItem, property: 'name' })}
              variant="outlined"
            />
            {IsEdition && (
              <>
                {onUserStoryEditorCLick && (
                  <Button
                    variant="text"
                    color="primary"
                    onClick={() => onUserStoryEditorCLick(item)}
                  >
                    User stories Editor
                  </Button>
                )}
                <CustomAccordion title="Documents">
                  <DocumentCollection
                    projectId={item.id}
                    onDownloaded={onDocumentDonwloaded}
                  />
                </CustomAccordion>
              </>
            )}
            {children?.(item, setItem)}
          </>
        );
      },
      [children, onDocumentDonwloaded, onUserStoryEditorCLick]
    );

    return (
      <FormWithButtons
        data={data}
        save={save}
        reset={reset}
        remove={remove}
        {...htmlAttributes}
      >
        {renderChildren}
      </FormWithButtons>
    );
  }
);
