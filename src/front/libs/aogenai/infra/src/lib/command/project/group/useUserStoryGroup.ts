import { IUserStoryGroupDto } from '@aogenai/domain';
import { QueryHookOptions, useQuery } from '@apollo/client';
import { GetUserStoryGroupQuery } from './cqrs';

interface Request {
  projectId: string;
  id: string;
}
interface Response {
  group: IUserStoryGroupDto;
}

export const useUserStoryGroup = (
  options?: QueryHookOptions<Response, Request>
) =>
  useQuery<Response, Request>(GetUserStoryGroupQuery, {
    ...options,
    skip:
      !options?.variables?.projectId ||
      !options?.variables?.id ||
      options?.skip,
  });
